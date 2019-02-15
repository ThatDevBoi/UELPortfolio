using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DB_Dont_Destroy_Start_Menu : MonoBehaviour
{
    public static bool created = false;

    // Use this for initialization
    void Start()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    
}
