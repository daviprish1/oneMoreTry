﻿using Scripts.SupportItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


abstract public class InteractiveScript : MonoBehaviour, IInteractive
{
    #region Variables
    public GameObject destination = null;               //need if this interact allow u to travel by 2 waypoints
    public string description = "";
    #endregion


    #region Unity functionality
    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }


    #endregion


    #region Support functionality
    public abstract void Interact(GameObject target);   //override in inherit classes
    #endregion
}
