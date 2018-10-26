//------------------------------------------------------------------------------------------------------------
// Simple 2D Goal and load scene
// Nina Alvir, UEL Gmaes, 2017
//------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NA_2D_Goal : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------------
    public string st_next_scene = "MS4406_wk07_01";

    //---------------------------------------------------------------------------------------------------------
    // Use this for initialization
	void Start()
    {
        // Disable the renderer and collider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }//-----

	//----------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update()
    {
        if (DD_Level_Manager.in_collectables < 1)
        {   // Re-enable the collider and renderer
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }
	}//-----

    //----------------------------------------------------------------------------------------------------------
    // Detect if something enters the trigger
    void OnTriggerEnter2D(Collider2D _cl_detected)
    {
        if (_cl_detected.tag == "Player")
        {
            SceneManager.LoadScene(st_next_scene);
        }
    }//-----

}//==========
