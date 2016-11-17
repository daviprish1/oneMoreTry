using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MedicineInteractScript : InteractiveScript
{
    #region Variables
    public bool isUsable = true;
    #endregion   

    #region Unity functionality
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}
    #endregion


    #region Support functionality
    public override void Interact(GameObject target)
    {
        GameObject.Find("Scripts").GetComponent<LevelScript>().isTextResultChanged = true;

        if (!isUsable)
        {
            GameObject.Find("TextResult").GetComponent<Text>().text = "Chest is Empty";
            return;
        }

        var player = target.GetComponent<PlayerScript>();
        if(player.curHealth == player.maxHealth)
        {
            GameObject.Find("TextResult").GetComponent<Text>().text = "Your health is full";
            return;
        }

        player.curHealth += 1;
        isUsable = false;
        GameObject.Find("TextResult").GetComponent<Text>().text = "You restored some health";
    }
    #endregion
}
