using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Bullet : MonoBehaviour {
    // Variables

    // public means properties can be modified in the inspector

    public float fl_speed = 5;
    public float fl_range = 10;
    public float fl_damage = 10;
    private Rigidbody2D RB_NPC;

    // Use this for initialization
    // ----------------------------------------------------------------------
    // Start is only called once when the object is first enabled in the scene
    void Start()
    {
        // Find the attched Rigidbody so we can manipulate the settings
        RB_NPC = GetComponent<Rigidbody2D>();

        // Calculate which way is forward 
        Vector2 _V2_direction = transform.TransformDirection(Vector3.right);

        //Set the velocity 
        RB_NPC.velocity = _V2_direction * fl_speed;

        // Set the lifetime of this object 
        Destroy(gameObject, fl_range / Mathf.Abs(fl_speed));
    }//----- 



    // ----------------------------------------------------------------------
    // Has this object collided with another 2D object?
    void OnTriggerEnter2D(Collider2D _cl_detected)
    {
        // Don't hit other bullets
        if (_cl_detected.tag != "Bullet")
        {
            // Send Damage to the touching object     
            _cl_detected.gameObject.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

    }//-----

}//==========
