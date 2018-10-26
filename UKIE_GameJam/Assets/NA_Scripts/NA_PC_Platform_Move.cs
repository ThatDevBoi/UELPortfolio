//---------------------------------------------------------------------------------------------------------------------------------------
// Simple 2D Platform Movement
// Nina Alvir, UEL Games, 2017
//---------------------------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NA_PC_Platform_Move : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------------
    // Variables
    public float fl_jump_force = 400;
    public float fl_acceleration = 20;
    public float fl_max_speed = 5;
    public bool bl_grounded;
    public LayerMask groundLayer;
    private Rigidbody2D rb_PC;

    //-------------------------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        rb_PC = GetComponent<Rigidbody2D>();
    }//-----

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        CheckGround();
        MovePC();
    }//-----

    //--------------------------------------------------------------------------------------------------------------------------------------
    void MovePC()
    {
        // When the PC is on the ground
        if (bl_grounded)
        {   // Apply jump force
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {   // Add jump force
                rb_PC.AddForce(Vector2.up * fl_jump_force);
                transform.parent = null;
            }
        }

        // in Either state - grounded or air
        // Stop horizontal movement if the opposite key is pressed
        if ((rb_PC.velocity.x > 0 && Input.GetAxis("Horizontal") < 0) || (rb_PC.velocity.x < 0 && Input.GetAxis("Horizontal") > 0))
            rb_PC.velocity = new Vector2(0, rb_PC.velocity.y);

        // Move the PC left and right keeping under the max speed using Hprizontal axis input
        if (Mathf.Abs(rb_PC.velocity.x) < fl_max_speed)
            rb_PC.AddForce(new Vector2(Input.GetAxis("Horizontal") * fl_acceleration, 0));
    }//-----

    //---------------------------------------------------------------------------------------------------------------------------------------
    void CheckGround()
    {
        // Cast a ray dowwards to check if the PC is standing on groud
        RaycastHit2D _rc_hit = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayer);

        if (_rc_hit.collider != null)
        {
            bl_grounded = true;
        }
        else
        {
            bl_grounded = false;
        }
    }//-----

    //----------------------------------------------------------------------------------------------------------------------------------------
    // Moving Platform - child Pc to platform
    void OnTriggerEnter2D(Collider2D _col_detected)
    {
        if (_col_detected.tag == "Moving")
        {   // Child the PC to the trigger
            transform.parent = _col_detected.transform;
        }
    }//-----

    void OnTriggerExit2D(Collider2D _col_detected)
    {
        if (_col_detected.tag == "Moving")
        {   // un child
            transform.parent = null;
        }
    }//-----
}//=====

       