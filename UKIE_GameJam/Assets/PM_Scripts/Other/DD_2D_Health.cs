using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_2D_Health : MonoBehaviour {

    // This script requires the object this is added to to have a 3D text object childed to it
    //---------------------------------------------------------------------------------------
    public float fl_HP = 100;
    public float fl_max_HP = 100;
    public bool bl_respawn_on_death;
    public Vector2 v2_respawn_position;
    private float fl_initial_HP;

    //---------------------------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {   // Set inital HP value and respawn position
        fl_initial_HP = fl_HP;
        v2_respawn_position = transform.position;		
	}//-----

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
	void Update () 
    {
        // Is the HP delpleted?
        if (fl_HP <= 0)
        {
            if (bl_respawn_on_death) 
            {   // respawn and reset HP
                transform.position = v2_respawn_position;
                fl_HP = fl_initial_HP;
            }
            else
            {   // Remove from the scene
                Destroy(gameObject);
            }
        }

        // Display HP in the attached text mesh component
        GetComponentInChildren<TextMesh>().text = Mathf.Round(fl_HP).ToString();
    }//------

    //---------------------------------------------------------------------------------------
    // Damage Reviever
    public void Damage(float _fl_damage)
    {    // Subtract damage from current HP
        fl_HP -= _fl_damage;
       
    }//-----

    //---------------------------------------------------------------------------------------
    // Health Reviever
    public void Health(float _fl_health)
    {   // add to HP       
        fl_HP += _fl_health;
        if (fl_HP > fl_max_HP) fl_HP = fl_max_HP; 
    }//-----


    //---------------------------------------------------------------------------------------
    // David Dorrington, UEL Games, 2017 
}//==========
