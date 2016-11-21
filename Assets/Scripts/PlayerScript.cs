using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    #region variables
    public float Speed = 0f;                                        //player speed
    private float moveX = 0f;                                       //x direction
    private float moveY = 0f;                                       //y direction

    public int maxHealth = 5;
    public int curHealth = 5;

    public bool movementLeft = false;                               //need for flip player sprite

    public List<Sprite> heartsSprites;                              //collection of sprites which present your current health condition
    public Image heartsUI;                                          //ref to ui what show one of the health sprites to the player

    private InteractiveScript curInteract = null;
    private List<InteractiveScript> InteractItems = new List<InteractiveScript>();      //list of all interactive items in area where u can interact with them

    public WeaponScript curWeapon = null;                           //current player weapon
    public List<WeaponScript> weapons = new List<WeaponScript>();   //list of all player weapons
    #endregion


    #region Unity functionality
    // Use this for initialization
    void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
        heartsUI.sprite = heartsSprites[this.curHealth];            //update health ui
        UpdateCurWeapon();                                          //update current weapon position

        moveX = Input.GetAxis("Horizontal");                        //get direction by x and y axis
        moveY = Input.GetAxis("Vertical");

        // Check if object move and flip sprite if it moves to left
        if (moveX != 0)
        {
			if (moveX > 0)
			{
				movementLeft = false;
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
			else
			{
				movementLeft = true;
				transform.localRotation = Quaternion.Euler(0, 180, 0);
			}
        }

        if (Input.GetKeyDown(KeyCode.Tab)) ChangeInteractTarget();

        //interact with items by pressing E (if this items exist)
        if (Input.GetKeyDown(KeyCode.E) && InteractItems.Count > 0) Interact();

		//basic shooting script. Shoot() parameter should be weapon desc variable
		if (Input.GetButton ("Fire1") && curWeapon != null)
			curWeapon.GetComponent<WeaponScript> ().Shoot();

        //switch weapons
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) SwitchWeapon(Input.GetAxis("Mouse ScrollWheel") > 0f);
    }

    void FixedUpdate()
    {
        //move player sprite
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * Speed, moveY * Speed);
    }

    //event rised where player enter into another body with collider and trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //get interacted item
        var interact = other.gameObject.GetComponent<InteractiveScript>();
        if (interact != null)   //check if this item exists
        {
            if (InteractItems.Count == 0) curInteract = interact;
            InteractItems.Add(interact);                                                            //add to interacted item collection
            GameObject.FindObjectOfType<LevelScript>().tooltipPanel.SetActive(true);                //show tooltip about potential interaction
            GameObject.Find("TextTarget").GetComponent<Text>().text = curInteract.description;      //show description about interaction
        }
    }

    //same like above, but rised when exit
    void OnTriggerExit2D(Collider2D other)
    {
        var interact = other.gameObject.GetComponent<InteractiveScript>();
        if (interact != null)
        {
            InteractItems.Remove(interact);                                                             //remove first item from collection
            if (InteractItems.Count == 0)
                GameObject.FindObjectOfType<LevelScript>().tooltipPanel.SetActive(false);               //if collectin is empty hide tooltip panel
            else
            {
                curInteract = InteractItems[0];
                GameObject.Find("TextTarget").GetComponent<Text>().text = curInteract.description;      //or show next element description
            }
        }
    }
    #endregion


    #region Support functionality
    //function for harm pity plater
    public void Damage(int dmg)
    {
        this.curHealth -= dmg;
        if (curHealth <= 0)                                         //If player health all gone
        {
            heartsUI.sprite = heartsSprites[this.curHealth];        //update health ui...
            Destroy(this.gameObject);                               //... AND KILL PLAYER WITH FIRE
        }
    }

    //function for interaction
    private void Interact()
    {
        curInteract.Interact(this.gameObject);                                  //just interact with first item in interactive collection
    }

    //for swithcing weapons
    private void SwitchWeapon(bool isForward)
    {
        if (weapons.Count <= 1) return;                                         //if u have 1 or 0 weapons - return
        int idx = weapons.IndexOf(curWeapon);                                   //index of current weapon
        if (isForward)                                                          //move forward in collection
        {
            if (idx >= 0 && idx < weapons.Count - 1) idx++;                     //update index
            else if (idx == weapons.Count - 1) idx = 0;                         //set 0 if index > size of collection
        }
        else                                                                    //pretty the same as above, but moving backward
        {
            if (idx > 0) idx--;
            else if (idx == 0) idx = weapons.Count - 1;
        }
        curWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = false;    //hide weapon what we want to switch
        curWeapon = weapons[idx];                                               //get new weapon
        curWeapon.gameObject.GetComponent<SpriteRenderer>().enabled = true;     //show this weapon
    }

    private void UpdateCurWeapon()
    {
        if (curWeapon == null) return;

        curWeapon.transform.position = gameObject.transform.position;
    }

    private void ChangeInteractTarget()
    {
        if (InteractItems.Count <= 1) return;
        int idx = InteractItems.IndexOf(curInteract);
        if (idx == InteractItems.Count - 1) idx = 0;
        else idx++;
        curInteract = InteractItems[idx];
        GameObject.Find("TextTarget").GetComponent<Text>().text = curInteract.description;
    }
    #endregion
}
