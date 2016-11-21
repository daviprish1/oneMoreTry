using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    #region Variables
    private GameObject target;

    public int maxHealth = 5;
    public int curHealth = 0;
    public int damage = 1;

    public Vector2 speed = new Vector2(8, 6);
    public Vector2 direction = new Vector2(1, 0);
    public bool movementLeft = false;

    private Vector2 movement = new Vector2();
    private Rigidbody2D rigidbodyComponent;

    public bool itsTriggered = false;
    #endregion


    #region Unity
    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        curHealth = maxHealth;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (itsTriggered && target != null)
        {
            Transform plTransform = target.GetComponent<Transform>();
            Transform oTransform = gameObject.GetComponent<Transform>();
            direction.x = plTransform.position.x < oTransform.position.x ? -1 : 1;
            direction.y = plTransform.position.y < oTransform.position.y ? -1 : 1;

            if (direction.x != 0)
            {
                if (direction.x > 0)
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

            movement = new Vector2(
              speed.x * direction.x,
              speed.y * direction.y);
        }
        
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
        movement = new Vector2();
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        ProjectilePhysics shot = otherCollider.gameObject.GetComponent<ProjectilePhysics>();
        if (shot != null)
        {
            Damage(shot.damage);

            // Destroy the shot
            Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
        }
    }
    #endregion


    #region Support
    public void Damage(int damageCount)
    {
        curHealth -= damageCount;

        if (curHealth <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }
    #endregion
}
