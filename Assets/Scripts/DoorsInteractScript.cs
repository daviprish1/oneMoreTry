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
    public string altSceneTarget = "";
    #endregion


    #region Unity functionality
    // Use this for initialization
    protected override void Start()
    {
        DontDestroyOnLoad(this);
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

        if (string.IsNullOrEmpty(sceneTarget))
        {
            Console.WriteLine("U forget set a scene name, or u just should disable this door");
            return;
        }


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(altSceneTarget))
        {
            SceneManager.LoadScene(sceneTarget, LoadSceneMode.Additive);
            StartCoroutine(WaitForSceneLoad(SceneManager.GetSceneByName(sceneTarget)));
            SceneManager.UnloadScene(SceneManager.GetSceneByName(altSceneTarget));
        }
        else
        {
            //SceneManager.LoadScene (altSceneTarget, LoadSceneMode.Additive);
            //StartCoroutine (WaitForSceneLoad (SceneManager.GetSceneByName (altSceneTarget)));
            SceneManager.UnloadScene(SceneManager.GetSceneByName(sceneTarget));
        }
    }

    public IEnumerator WaitForSceneLoad(Scene scene)
    {
        while (!scene.isLoaded)
        {
            yield return null;
        }
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(scene);
    }


    #endregion
}
