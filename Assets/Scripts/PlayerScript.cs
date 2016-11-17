using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    #region variables
    public float Speed = 0f;
    private float moveX = 0f;
    private float moveY = 0f;

    public int maxHealth = 5;
    public int curHealth = 5;

    public bool movementLeft = false;

    public List<Sprite> heartsSprites;
    public Image heartsUI;

    private List<InteractiveScript> InteractItems = new List<InteractiveScript>();
    #endregion


    #region Unity functionality
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        heartsUI.sprite = heartsSprites[this.curHealth];

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        // Check if object move and flip sprite if it moves to left
        if (moveX != 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = movementLeft = moveX < 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && InteractItems.Count > 0) Interact();
        
	}

    void FixedUpdate()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * Speed, moveY * Speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var interact = other.gameObject.GetComponent<InteractiveScript>();
        if (interact != null)
        {
            InteractItems.Add(interact);
            GameObject.FindObjectOfType<LevelScript>().tooltipPanel.SetActive(true);
            GameObject.Find("TextTarget").GetComponent<Text>().text = InteractItems[0].description;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var interact = other.gameObject.GetComponent<InteractiveScript>();
        if (interact != null)
        {
            InteractItems.Remove(interact);
            if(InteractItems.Count == 0) GameObject.FindObjectOfType<LevelScript>().tooltipPanel.SetActive(false);
        }
    }
    #endregion


    #region Support functionality
    private void Damage(int dmg)
    {
        this.curHealth -= dmg;
        if (curHealth <= 0)
        {
            heartsUI.sprite = heartsSprites[this.curHealth];
            Destroy(this.gameObject);
        }
    }

    private void Interact()
    {
        InteractItems[0].gameObject.GetComponent<InteractiveScript>().Interact(this.gameObject);
    }
    #endregion
}
