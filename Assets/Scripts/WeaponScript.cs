using UnityEngine;
using System.Collections;
using Scripts.SupportItems;


//TODO: IMPLEMENT THIS SCRIPT. BUT WHY??
public class WeaponScript : MonoBehaviour {
    #region Variables
    public int damage = 0;

    public Transform shotPrefab;
	public float shootingRate;
    private float shootCooldown = 0;
	private Transform muzzleRef; 
	private float ctime;

    #endregion

    // Use this for initialization
    void Start () {
						
	}
	
	// Update is called once per frame
	void Update () {
		ctime = Time.time;
	}

	public void Shoot () {
		if (ctime > shootCooldown) {			
			var bullet = Instantiate(shotPrefab, transform.FindChild("Muzzle").position, transform.FindChild("Muzzle").rotation) as Transform;
            bullet.gameObject.GetComponent<ProjectilePhysics>().damage = damage;				
			shootCooldown = ctime + shootingRate;
            var asrc = GetComponent<AudioSource>();
            if (asrc != null) asrc.Play();
        }
	}

}
