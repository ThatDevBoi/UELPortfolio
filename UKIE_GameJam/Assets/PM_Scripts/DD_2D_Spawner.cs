using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Spawner : MonoBehaviour 
{
    //---------------------------------------------------------------------------------------
    // Variables
    public GameObject GO_Spawnee;
    public bool bl_infinite = true;
    public bool bl_destroy_when_done = false;
    public int in_spawn_total = 10;
    public float fl_cooldown = 1;
    private float fl_next_spawn_time;

    //---------------------------------------------------------------------------------------
   	// Use this for initialization
	void Start () 
    {
        // Set the initial spawn time
        fl_next_spawn_time = fl_cooldown + Time.time;		
	}//-----

    //---------------------------------------------------------------------------------------
   	// Update is called once per frame
	void Update () 
    {
        // has the cooldown time passed and are there objects left to spawn 
        if ( Time.time > fl_next_spawn_time && (bl_infinite || in_spawn_total > 0) )
        {
            // Create a clone of the spawneee at the position and rotation of the atatched object
            Instantiate(GO_Spawnee, transform.position, transform.rotation);

            // Set the next spawn time
            fl_next_spawn_time = Time.time + fl_cooldown;

            // reduce the number of spawnees left 
            if (!bl_infinite) in_spawn_total--;
        }

        // Remove this from the scene when all items are spawned
        if (bl_destroy_when_done && in_spawn_total < 1) Destroy(gameObject);
	
	}//-----
    
    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//=========