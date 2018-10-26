//---------------------------------------------------------------------------------------
// Simple 2D Pick Ups
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_PickUp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Add one to the collectables in the level Manager
        DD_Level_Manager.in_collectables++;

    }//------
  
    //---------------------------------------------------------------------------------------
    // Detect if something enters the Trigger
    void OnTriggerEnter2D(Collider2D _cl_detected)
    {
        if (_cl_detected.tag == "Player")
        {
            // Update the stats in the level Manager
            DD_Level_Manager.in_collectables--;
            DD_Level_Manager.in_score += 50;

            Destroy(gameObject);
        }
    }//-----

}//=========
