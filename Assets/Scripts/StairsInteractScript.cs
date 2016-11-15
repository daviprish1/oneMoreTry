using UnityEngine;
using System.Collections;
using System;

public class StairsInteractScript : InteractiveScript {
    #region Variables
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
        target.transform.position = destination.transform.position;
    }
    #endregion
}
