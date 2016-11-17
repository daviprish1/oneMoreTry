using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour {

    #region Variables
    public GameObject tooltipPanel;

    public bool isTextResultChanged;
    private bool timerStarted = false;
    private float timerMaxTime = 4f;
    private float timerTimeLeft;
    #endregion


    #region Unity functionality
    // Use this for initialization
    void Start () {
        timerTimeLeft = timerMaxTime;
        isTextResultChanged = false;        
        tooltipPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        TextResultChecker();
    }
    #endregion


    #region Support functionality
    private void TextResultChecker()
    {
        if (isTextResultChanged || timerStarted)
        {
            timerStarted = true;
            timerTimeLeft = isTextResultChanged ? timerMaxTime : timerTimeLeft - Time.deltaTime;
            if (timerTimeLeft <= 0 && tooltipPanel.activeSelf)
            {
                GameObject.Find("TextResult").GetComponent<Text>().text = "";
                timerTimeLeft = timerMaxTime;
                timerStarted = false;
            }
            isTextResultChanged = false;
        }
    }

    #endregion
}
