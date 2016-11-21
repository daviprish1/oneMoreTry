using UnityEngine;
using System.Collections;

public class AggroZone : MonoBehaviour {
    #region Unity
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a player?
        var player = otherCollider.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            gameObject.GetComponentInParent<EnemyScript>().itsTriggered = true;
        }
        
    }
    #endregion
}
