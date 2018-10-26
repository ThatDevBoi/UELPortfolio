using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Camera_Follow : MonoBehaviour {

    public GameObject GO_Target;

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
	void Update () 
    {
        // if the target is in the scene match the x and y position but keep z distance
        if (GO_Target)
            transform.position = new Vector3(GO_Target.transform.position.x, GO_Target.transform.position.y, transform.position.z);       
	}//-----

    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========
