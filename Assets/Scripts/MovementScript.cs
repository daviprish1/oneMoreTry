using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
    #region Variables
    public float speedX = 0f;                                       //object speed
    public float speedY = 0f;                                       //object speed
    protected float moveX = 0f;                                       //x direction
    protected float moveY = 0f;                                       //y direction

    //public bool movementLeft = false;                               //need for flip object sprite
    #endregion


    #region Unity
    // Use this for initialization
    protected virtual void Start () {
	
	}

    // Update is called once per frame
    protected virtual void Update () {
	
	}

    protected virtual void FixedUpdate()
    {
        //move object
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speedX, moveY * speedY);
    }
    #endregion
}
