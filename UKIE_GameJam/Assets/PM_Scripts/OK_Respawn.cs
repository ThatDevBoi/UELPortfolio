using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OK_Respawn : MonoBehaviour {

    public string st_next_scene = "PM_MS4406_WK07_01";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D _cl_detected)
    {
        if (_cl_detected.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(st_next_scene);

            DD_Level_Manager.in_collectables = 0;
            DD_Level_Manager.in_score = 0;
        }
    }//--------------------------------
}
