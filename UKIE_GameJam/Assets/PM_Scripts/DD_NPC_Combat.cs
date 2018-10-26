// ----------------------------------------------------------------------
// -------------------- 2D NPC Combat
// -------------------- David Dorrington, UEL Games, 2016
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DD_NPC_Combat : MonoBehaviour
{

    // ----------------------------------------------------------------------
    // Variables
    public float fl_min_range = 4;
    public float fl_max_range = 8;
    public float fl_damage = 10;
    public float fl_cool_down = 0.1F;
    public float fl_accuracy = 100;
    public int in_ammo = 10;
    public float fl_HP = 100;
    public float fl_speed;
    public string st_target_class;
    public GameObject go_target;
    public GameObject go_leader;
    public bool bl_follow_leader = true;
    private float fl_delay;
    private Rigidbody2D rb_NPC;


    // Attack Properties
    public GameObject GO_bullet;


    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        rb_NPC = GetComponent<Rigidbody2D>();
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

        FindTarget();
        if (go_target && Vector2.Distance(go_target.transform.position, transform.position) < fl_max_range)
        {
            SendAttack();
            Move();
        }
        else
        {

            if (go_leader)
                FollowLeader();
            else
                rb_NPC.velocity = Vector2.zero;

        }


        // Destroy this object if HP has gone
        if (fl_HP < 1) Destroy(gameObject);

    }//-----


    // ----------------------------------------------------------------------
    void FollowLeader()
    {

        // Look at Target
        Vector3 _dir = go_leader.transform.position - transform.position;
        float _angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);

        // if the target is out of range move towards it
        if (Vector2.Distance(go_leader.transform.position, transform.position) > fl_min_range && Vector2.Distance(go_leader.transform.position, transform.position) < fl_max_range)
        {
            rb_NPC.velocity = (Vector2)transform.TransformDirection(Vector3.right) * fl_speed;
        }
        else
        {
            rb_NPC.velocity = Vector2.zero;
        }


    }//------



    // ----------------------------------------------------------------------
    void Move()
    {
        // Look at Target
        Vector3 _dir = go_target.transform.position - transform.position;
        float _angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);

        // if the target is out of range move towards it
        if (Vector2.Distance(go_target.transform.position, transform.position) > fl_min_range && Vector2.Distance(go_target.transform.position, transform.position) < fl_max_range)
        {
            rb_NPC.velocity = (Vector2)transform.TransformDirection(Vector3.right) * fl_speed;
        }
        else
        {
            rb_NPC.velocity = Vector2.zero;
        }

    }//------

    // ----------------------------------------------------------------------
    void SendAttack()
    {
        // Has the cooldown period passed and is there ammo
        if (Time.time > fl_delay && in_ammo > 0)
        {
            // Is the target in range
            if (Vector2.Distance(go_target.transform.position, transform.position) < fl_max_range)
            {
                // create a bullet clone                           
                GameObject _bullet = Instantiate(GO_bullet, transform.position + transform.TransformDirection(Vector2.right * 0.8F), transform.rotation) as GameObject;

                // Accuracy rotation
                if (fl_accuracy < 100) _bullet.transform.Rotate(0, 0, Random.Range(-(100 - fl_accuracy) / 3, (100 - fl_accuracy) / 3));

                // Set the bullet damage
              //  _bullet.GetComponent<DD_Projectile>().fl_damage = fl_damage;

                // Add the cool down time 
                fl_delay = Time.time + fl_cool_down;

                // Reduce Ammo 
                in_ammo -= 1;
            }
        }

    }//-----

    // ----------------------------------------------------------------------
    // Damage Reviever
    public void Damage(int _in_hit)
    {
        fl_HP -= _in_hit;
        // Dislpay HP
        GetComponentInChildren<TextMesh>().text = fl_HP.ToString();
    }//-----



    // ----------------------------------------------------------------------
    void FindTarget()
    {
        // If no target is set then find the closest one
        if (!go_target || go_target == null)
        {
            // Create a List of potential targets
            GameObject[] _GO_Enemies = GameObject.FindGameObjectsWithTag(st_target_class);

            // Are their any tagged targets in the scene?
            if (_GO_Enemies.Length > 0)
            {
                float _dist = Mathf.Infinity;
                GameObject _GO_nearest = null;

                // Loop through the list of targets
                foreach (GameObject _GO in _GO_Enemies)
                {
                    float _cur_dist = Vector2.Distance(_GO.transform.position, transform.position);
                    if (_cur_dist < _dist)
                    {
                        _GO_nearest = _GO;
                        _dist = _cur_dist;
                    }
                }
                // Set the Target
                go_target = _GO_nearest;
            }
        }
    }//---- 

}//=====
