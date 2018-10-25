using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_NPC_Chase : MonoBehaviour
{
    // Combat Variables 
    public GameObject go_projectile;
    public float fl_attack_range = 20;
    public float fl_cool_down = 1;

    private float fl_next_shot_time;

    // Movement Variables
    public float fl_chase_dist_max = 10;
    public float fl_chase_dist_min = 3;
    public float fl_chase_speed = 3;

    public GameObject go_target;
    private CharacterController cc_NPC;

    // Use this for initialization
    void Start ()
    {
        cc_NPC = GetComponent<CharacterController>();
        // if no target is set find the first tagged as the enemy 
        if (!go_target) go_target = GameObject.FindWithTag("Player");
	}//-------------------------
	
	// Update is called once per frame
	void Update ()
    {
        NPC_Move();
        AttackTarget();
	}//-------------------
    void NPC_Move()
    {
        // Is the target in chasing Range 
        if (Vector3.Distance(transform.position, go_target.transform.position) < fl_chase_dist_max)
        {   // Face the Target 
            transform.LookAt(go_target.transform.position);
            // is the target Further away then minimum chase distance 
            if (Vector3. Distance (transform.position, go_target.transform.position) > fl_chase_dist_min)
            {
                // Move Towards the target 
                cc_NPC.SimpleMove(fl_chase_speed * transform.TransformDirection(Vector3.forward));
            }
        }
        else
        {
            // Stop Moving 
            cc_NPC.SimpleMove(Vector3.zero);
        }    
    }//--------------------------------------

    void AttackTarget()
    {
        if (Time.time > fl_next_shot_time && 
            Vector3.Distance(transform.position, go_target.transform.position) < fl_attack_range)
        {
            // Face the target 
            transform.LookAt(go_target.transform.position);
            // Spawn an Arrow 
            GameObject _go_projectile_clone = Instantiate(go_projectile,
                transform.position + transform.TransformDirection(new Vector3(0, 0, 1F)), transform.rotation);

            // Reset Cooldown
            fl_next_shot_time = Time.time + fl_cool_down;
        }
    }//---------------------------------
}//======================
