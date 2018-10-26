using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Asteroid : MonoBehaviour {

    //---------------------------------------------------------------------------------------
    public float fl_speed = 1;
    private Rigidbody2D RB_asteroid;

    //---------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Find the attched Rigidbody so we can manipulate the settings
        RB_asteroid = GetComponent<Rigidbody2D>();

        // Rotate the Asteroid a random direction
        transform.Rotate(0, 0, Random.Range(0, 360));

        // Calculate which way is forward 
        Vector2 _V2_direction = transform.TransformDirection(Vector3.right);


        //Set the velocity 
        RB_asteroid.velocity = _V2_direction * fl_speed;

    }//-----
  
    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========
