using UnityEngine;
using System.Collections;

public class EnemyScript : MovementScript
{
    #region Variables
    private GameObject target;

    public int maxHealth = 5;
    public int curHealth = 0;
    public int damage = 1;

    public bool movementLeft = false;

    private Rigidbody2D rigidbodyComponent;

    public bool itsTriggered = false;
    #endregion


    #region Unity
    // Use this for initialization
    protected override void Start()
    {
        //DontDestroyOnLoad(gameObject);
        curHealth = maxHealth;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (itsTriggered && target != null)
        {
            Transform plTransform = target.GetComponent<Transform>();
            Transform oTransform = gameObject.GetComponent<Transform>();
            moveX = plTransform.position.x < oTransform.position.x ? -1 : 1;
            moveY = plTransform.position.y < oTransform.position.y ? -1 : 1;

            if (moveX > 0)
            {
                movementLeft = false;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveX < 0)
            {
                movementLeft = true;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

        }

    }

    protected override void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        if (itsTriggered && target != null) base.FixedUpdate();
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
