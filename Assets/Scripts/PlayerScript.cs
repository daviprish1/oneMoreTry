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

    public List<Sprite> heartsSprites;
    public Image heartsUI;

    private List<Collider2D> InteractItems = new List<Collider2D>();
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
            InteractItems.Add(other);
            GameObject.FindObjectOfType<LevelScript>().tooltipPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var interact = other.gameObject.GetComponent<InteractiveScript>();
        if (interact != null)
        {
            InteractItems.Remove(other);
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
