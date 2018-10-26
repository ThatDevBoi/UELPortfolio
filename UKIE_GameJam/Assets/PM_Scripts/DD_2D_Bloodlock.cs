using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Bloodlock : MonoBehaviour {

    // This script will enable / disable an object whene several other object are removed from the scene

    // Variables
    public GameObject[] Go_Activators;
    public GameObject Go_target_object;
    public bool bl_enable; 

    //------------------------------------------------------------------------------------------------------
    //Use this for initialization
	void Start ()
    {
        //Disable the target object if we are to enable it
        if (bl_enable) Go_target_object.SetActive(false);
	}//------------

	//---------------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update ()
    {
        //local variable of total number of activation objects we started with
        int _in_objects_left = Go_Activators.Length;

        //loop through the list of activation objects
        foreach (GameObject _GO in Go_Activators)
        {
            //subtract 1 from the total if the gameobject has been destroyed
            if (!_GO) _in_objects_left--;
        }
        // are all the objects destroyed?
        if (_in_objects_left < 1)
        {
            if (bl_enable) //are we enabling or disabling the target object
                Go_target_object.SetActive(true);
            else
                Go_target_object.SetActive(false);
        }

    }//-----------------

    //-----------------------------------------------------------------
}//---------------------------------------------------------------------------------------
