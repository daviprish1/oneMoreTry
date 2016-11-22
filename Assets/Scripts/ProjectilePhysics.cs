using UnityEngine;
using System.Collections;

public class ProjectilePhysics : MonoBehaviour {

	private Rigidbody2D bullet; 
	public float shotspeed = 15;
    public int damage = 1;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

		bullet = GetComponent<Rigidbody2D> ();
		if (GameObject.Find ("Player").GetComponent<PlayerScript> ().movementLeft) shotspeed  *= -1;		
		bullet.velocity = new Vector2(shotspeed, 0);
		Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
