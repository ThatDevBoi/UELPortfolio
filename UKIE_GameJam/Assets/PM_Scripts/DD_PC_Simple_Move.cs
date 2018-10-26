//---------------------------------------------------------------------------------------
// Simple 2D PC Movement 
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_PC_Simple_Move : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Variable Definitions
    public float fl_PC_speed = 2;
    public float fl_rotation_rate = 90;
    public bool bl_Rotate = true;
    private Rigidbody2D rb_PC;
    private Vector2 v2_velocity;
   
    //---------------------------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {        
        // Get a reference to the attached rigidbody
        rb_PC = GetComponent<Rigidbody2D>();
	}//-----

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        // Switch movement mode 
        if (bl_Rotate)
            RotateMove();
        else
            MoveXY();
    }//-----   

    //---------------------------------------------------------------------------------------
    void RotateMove()
    {
        // Rotate with H axis    
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * fl_rotation_rate * Time.deltaTime);

        // Move with V axis     
        rb_PC.velocity = transform.TransformDirection(Vector2.right) * fl_PC_speed * Input.GetAxis("Vertical");
    }//-----


    //---------------------------------------------------------------------------------------
    void MoveXY()
    {
        // Calculate the velocity vector based on player input 
        v2_velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * fl_PC_speed;

        // Update the position of the PC using the velocity vector
        rb_PC.MovePosition(rb_PC.position + v2_velocity);
    }//------
    
}//==========