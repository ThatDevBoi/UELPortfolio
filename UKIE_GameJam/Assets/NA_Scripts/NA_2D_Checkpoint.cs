using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NA_2D_Checkpoint : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Detect if something enter the Trigger
    void OnTriggerEnter2D(Collider2D _cl_detected)
    {
        // Is the trigger the PC
        if (_cl_detected.tag == "Player")
        {
            // Set the new respawn position in the healt script
            _cl_detected.GetComponent<DD_2D_Health>().v2_respawn_position = transform.position;

            // Remove this from the scene
            Destroy(gameObject);
        }
    }//-----

    //----------------------------------------------------------------------------------------
    // 2D Checkpoint
    // Nina Alvir, UEL Gmaes, 2017
}//==========
