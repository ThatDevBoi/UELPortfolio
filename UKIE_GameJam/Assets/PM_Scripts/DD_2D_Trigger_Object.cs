using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Trigger_Object : MonoBehaviour {

    // ----------------------------------------------------------------------
    // Variables in this script
    public bool bl_enable;
    public bool bl_destroy_trigger_when_activated;
    public GameObject GO_target_object;
    public GameObject GO_activation_object;
    public float fl_delay = 0.5F;
    private bool bl_triggered;
    private float fl_trigger_time;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Disable the target object if we are to enable it
        if (bl_enable) GO_target_object.SetActive(false);
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // If this trigger has been correctly activated swap the target object's state
        if (bl_triggered && Time.time > fl_trigger_time)
        {
            if (bl_enable)
                GO_target_object.SetActive(true);
            else
                GO_target_object.SetActive(false);

            // Stop the trigger by setting the next trigger time to the infinity 
            fl_trigger_time = Mathf.Infinity;

            //Destroy this object when complete 
            if (bl_destroy_trigger_when_activated) Destroy(gameObject);
        }
    }//-----

    // ----------------------------------------------------------------------
    // Detect if something enters the Trigger
    void OnTriggerEnter2D(Collider2D _cl_detected)
    {
        // If the correct activation object has entered this, enable / disable the target object 
        if (_cl_detected.gameObject == GO_activation_object)
        {
            bl_triggered = true;

            fl_trigger_time = Time.time + fl_delay;
        }
    }//------ 

    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========
