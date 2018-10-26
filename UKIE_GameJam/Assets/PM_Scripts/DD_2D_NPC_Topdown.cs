using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_NPC_Topdown : MonoBehaviour {   

    // Movement  Variables
    public float fl_speed = 1; // 0 for static
    public float fl_min_move_range = 2;
    public float fl_max_move_range = 5;

   // Attack Properties
    public float fl_attack_range = 5;
    public float fl_cool_down = 0.1F;
    public GameObject GO_target;
    public GameObject GO_bullet;       
  
    private float fl_delay;
    private Rigidbody2D RB_NPC;
    
    //---------------------------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {   
        RB_NPC = GetComponent<Rigidbody2D>();		
	}//-----

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
	void Update () 
    {
        MoveNPC();
        AttackTarget();
    }//-----    

    //---------------------------------------------------------------------------------------
   void AttackTarget()
    {
       // Has the cooldown period passed 
        if (Time.time > fl_delay)
        {
            // Is the target in range
            if (Vector2.Distance(GO_target.transform.position, transform.position) < fl_attack_range)
            {
                // create a bullet in front of this NPC
                Instantiate(GO_bullet, transform.position + transform.TransformDirection(Vector2.right), transform.rotation);
                // Reset cooldown timer
                fl_delay = Time.time + fl_cool_down;
            }
        }
    }//-----

    //---------------------------------------------------------------------------------------
    void MoveNPC()
    {
        // Look at Target - This uses the trigonometric function Tan to calcucualte the angle to rotate to look at the PC
        Vector2 _v2_difference = GO_target.transform.position - transform.position;
        float _fl_angle = Mathf.Atan2(_v2_difference.y, _v2_difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_fl_angle, Vector3.forward);

        //calculate the distance to the Target (e.g PC) 
        float _fl_distance = Vector2.Distance(GO_target.transform.position, transform.position);

        // is the target is in movement range
        if (_fl_distance < fl_max_move_range && _fl_distance > fl_min_move_range)
        {   //Move towards Target
            RB_NPC.velocity = transform.TransformDirection(Vector2.right) * fl_speed;
        }
        else
        {   // Stop Moving
            RB_NPC.velocity = Vector2.zero;
        }
    }//------  

    //---------------------------------------------------------------------------------------
    // NPC control script. Follows and attacks the PC / target
    // David Dorrington, UEL Games, 2017 
}//==========
