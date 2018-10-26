using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Trigger_Send_Message : MonoBehaviour {

    //---------------------------------------------------------------------------------------
    public string st_message = "Health";
    public int in_value = 30;
    public bool bl_Destroy_when_Hit = true;
    
    //---------------------------------------------------------------------------------------
    void Start()
    {
        // Display HP in the attached text mesh component
        GetComponentInChildren<TextMesh>().text = st_message;
    }//-----


    //---------------------------------------------------------------------------------------
    // Detect if something enters the Trigger
    void OnTriggerStay2D(Collider2D _cl_detected)
    {
        if (_cl_detected.tag == "Player")
        {
            if (bl_Destroy_when_Hit)
            {
                // Send one message of the amount specified 
                _cl_detected.gameObject.SendMessage(st_message, in_value, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            else
            {
                // continue to send messages at the amount per second
                _cl_detected.gameObject.SendMessage(st_message, in_value * Time.deltaTime, SendMessageOptions.DontRequireReceiver);                
            }
        }
    }//-----

    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========