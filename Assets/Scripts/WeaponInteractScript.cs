using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class WeaponInteractScript : InteractiveScript
{

    #region Support functionality
    public override void Interact(GameObject target)
    {
        PlayerScript ps = target.GetComponent<PlayerScript>();
        if (ps == null) return;

        foreach(var w in ps.weapons)
        {
            //check if playeer alredy this weapon by weapon sprite
            if(gameObject.GetComponent<SpriteRenderer>().sprite == w.gameObject.GetComponent<SpriteRenderer>().sprite)
            {
                GameObject.Find("Scripts").GetComponent<LevelScript>().isTextResultChanged = true;
                GameObject.Find("TextResult").GetComponent<Text>().text = "You already have this weapon";
                return;
            }
        }

        ps.weapons.Add(gameObject.GetComponent<WeaponScript>());                        //add weapon to player weapon collection
        ps.SendMessage("OnTriggerExit2D", gameObject.GetComponent<BoxCollider2D>());    //activate trigger "OnTriggerExit2D" what remove this item from interacted collection
        gameObject.GetComponent<BoxCollider2D>().enabled = false;                       //remove box collider
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";          //sorting layer player
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;                     //and set order > than player order

        //if player dont have weapons set this by his current weapon
        if (ps.curWeapon == null)                                                       
        {
            ps.curWeapon = gameObject.GetComponent<WeaponScript>();
        }
        else gameObject.GetComponent<SpriteRenderer>().enabled = false;                 //or just hide this weapon

    }
    #endregion
}
