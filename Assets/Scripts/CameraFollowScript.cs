using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    #region Variables
    public Transform target;

    private Vector3 offset;
    #endregion


    #region Unity functionality
    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        offset = new Vector3(0, 0, -10);   
		DontDestroyOnLoad (this);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //Called after Update()
    void LateUpdate()
    {
        if(target != null) transform.position = target.transform.position + offset;
    }
    #endregion
}
