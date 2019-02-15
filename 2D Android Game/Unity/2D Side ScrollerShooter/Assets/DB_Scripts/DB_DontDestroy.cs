using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_DontDestroy_Money : MonoBehaviour
{

    private static bool Created = false;

	// Use this for initialization
	void Start ()
    {
        if (!Created)
        {
            DontDestroyOnLoad(gameObject);
            Created = true;
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
