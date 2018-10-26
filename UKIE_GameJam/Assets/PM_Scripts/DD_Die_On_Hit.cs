//---------------------------------------------------------------------------------------
// Simple 2D  Destroy object if it hits another game object
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Die_On_Hit : MonoBehaviour {

    ///---------------------------------------------------------------------------------------
    public string st_no_effect_tag = "";

    // ----------------------------------------------------------------------
    // Has this object collided with another 2D object?
    void OnCollisionEnter2D(Collision2D _cl_detected)
    {
        if (_cl_detected.gameObject.tag != st_no_effect_tag)
        {
            // Remove this game object from the scene
            Destroy(gameObject);
        }
    }//-----
    
}//==========
