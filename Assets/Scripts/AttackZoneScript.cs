using UnityEngine;
using System.Collections;

public class AttackZoneScript : MonoBehaviour {
    #region Variables
    private float attackCd = 2f;
    private float curAttackCd;
    private PlayerScript target = null;
    #endregion


    #region Unity
    void Start()
    {
        curAttackCd = 0f;
    }

    void Update()
    {
        if (curAttackCd > 0)
        {
            curAttackCd -= Time.deltaTime;
        }
        Attack();
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.Equals(GameObject.Find("Player")))
            target = otherCollider.gameObject.GetComponent<PlayerScript>();
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.Equals(GameObject.Find("Player")))
            target = null;
    }
    #endregion

    #region Support
    public void Attack()
    {
        if (target != null && CanAttack)
        {
            curAttackCd = attackCd;
            target.Damage(gameObject.GetComponentInParent<EnemyScript>().damage);
        }
    }

    public bool CanAttack
    {
        get
        {
            return curAttackCd <= 0f;
        }
    }
    #endregion
}
