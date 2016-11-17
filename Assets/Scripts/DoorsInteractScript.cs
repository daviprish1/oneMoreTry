using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorsInteractScript : InteractiveScript
{
    #region Variables
    public bool isUsable = true;
    public string sceneTarget = "";
    #endregion 


    #region Unity functionality
    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }
    #endregion


    #region Support functionality
    public override void Interact(GameObject target)
    {
        GameObject.Find("Scripts").GetComponent<LevelScript>().isTextResultChanged = true;

        if (!isUsable)
        {
            GameObject.Find("TextResult").GetComponent<Text>().text = "Door is locked";
            return;
        }

        if(string.IsNullOrEmpty(sceneTarget))
        {
            Console.WriteLine("U forget set a scene name, or u just should disable this door");
            return;
        }

        //TODO: Load next scene
        //SceneManager.LoadScene(sceneTarget); 
    }
    #endregion
}
