//---------------------------------------------------------------------------------------
// Simple 2D Projectile
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Projectile : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Variables - public values can be modified in the inspector on instances of objects
    public float fl_speed = 5;
    public float fl_range = 10;
    public bool bl_destroy_on_hit = true;
    private Rigidbody2D rb_projectile;

    //---------------------------------------------------------------------------------------
    // Use this for initialization - Commands here are only called once when objects appear in the game
    void Start()
    {
        // Get the reference to the rigidbody physics controller for this object 
        rb_projectile = GetComponent<Rigidbody2D>();

        // Set velocity to move forwards at the speed defined above
        rb_projectile.velocity = transform.TransformDirection(Vector2.right) * fl_speed;

        // Remove this object from the scene when the range is reached 
        Destroy(gameObject, fl_range / Mathf.Abs(fl_speed));
    }//-----    

    //---------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet if it hits something (if set)
        if (bl_destroy_on_hit) Destroy(gameObject);
    }//------
    
}//==========