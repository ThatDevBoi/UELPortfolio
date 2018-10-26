//---------------------------------------------------------------------------------------
//  2D Respawn on hit
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Respawn_On_Hit : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Variable Definitions
    public Vector2 v2_respawn_position;
    public float fl_respawn_delay = 2;
    private float fl_next_spawn_time;
    private SpriteRenderer sp_PC;
    private CircleCollider2D cl_PC;

    //---------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Set the respawn position to the start vector
        v2_respawn_position = transform.position;
        // Get a reference to the sprite renderer component and collider
        sp_PC = GetComponent<SpriteRenderer>();
        cl_PC = GetComponent<CircleCollider2D>();
    }//-----

    //---------------------------------------------------------------------------------------
    void Update()
    {
        // Has the delay time passed
        if (fl_next_spawn_time < Time.time && !sp_PC.enabled)
        {
            // Make the sprite visible and enable collision
            sp_PC.enabled = true;
            cl_PC.enabled = true;
        }        
    }//-----

    //---------------------------------------------------------------------------------------
    // Detect if this object hits something with a collider
    private void OnCollisionEnter2D(Collision2D _cl_detected)
    {
        //Reposition the gameobject to the original position
        transform.position = v2_respawn_position;

        // Set the delay time & turn off the sprite renderer and collider
        fl_next_spawn_time = Time.time + fl_respawn_delay;
        sp_PC.enabled = false;
        cl_PC.enabled = false;
    }//-----

}//============
