using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Send_Damage_On_Hit : MonoBehaviour {
    //---------------------------------------------------------------------------------------
    public float fl_damage = 10;

  
    //---------------------------------------------------------------------------------------
    // Has this object collided with another 2D object?
    void OnCollisionEnter2D(Collision2D _cl_detected)
    {
        // Send a damaage message to the object we hit
        _cl_detected.gameObject.SendMessage( "Damage", fl_damage, SendMessageOptions.DontRequireReceiver );

    }//------

    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========