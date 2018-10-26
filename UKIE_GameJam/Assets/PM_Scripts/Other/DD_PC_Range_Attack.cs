//---------------------------------------------------------------------------------------
// Simple 2D PC shoot 
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_PC_Range_Attack : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Attack Properties
    public GameObject go_projectile;
    public int in_ammo = 100;
    public bool  bl_infinite_ammo = true;
    public float fl_cool_down = 1F;
    private float fl_next_shot_time;
    
    //---------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        Attack();
    }//-----

    //---------------------------------------------------------------------------------------
    // Custom function for attack
    void Attack()
    {
        // Has the fire button (CTRL or mouse) been pressed and cooldown delay time passed 
        if (Input.GetButtonDown("Fire1") && Time.time > fl_next_shot_time)
        {
            if (bl_infinite_ammo)
            {
                // Create a bullet 1 at unit in front of the PC
                Instantiate(go_projectile, transform.position + transform.TransformDirection(Vector2.right), transform.rotation);               
            }
            else  if (in_ammo > 0) 
            {
                /// Create a bullet 1 unit in front of the PC
                Instantiate(go_projectile, transform.position + transform.TransformDirection(Vector2.right), transform.rotation);
                
                // Reduce Ammo 
                in_ammo--;
            }
            // Reset cooldown time
            fl_next_shot_time = Time.time + fl_cool_down;           
        }
    } // -----
}//==========
