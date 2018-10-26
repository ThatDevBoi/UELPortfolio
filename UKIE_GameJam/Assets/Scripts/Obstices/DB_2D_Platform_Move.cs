using UnityEngine;
using System.Collections;

public class DB_2D_Platform_Move : MonoBehaviour
{

    // ----------------------------------------------------------------------
    // Variables
    public float fl_speed = 1;
    public Vector2 V2_end_position;
    private Vector2 V2_start_position;
    private bool bl_forward = true;
    private float fl_path_length;
    private float fl_start_time;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        V2_start_position = transform.position;
        
        fl_path_length = Vector2.Distance(V2_start_position, V2_end_position);
        fl_start_time = Time.time;
    }//-----	

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // temp movement variables
        float _fl_dist_travelled = (Time.time - fl_start_time) * fl_speed;
        float _fl_step = _fl_dist_travelled / fl_path_length;

        // Move towards end pos
        if (bl_forward)
        {
            transform.position = Vector2.Lerp(V2_start_position, V2_end_position, _fl_step);
            if (Vector2.Distance(transform.position, V2_end_position) < 0.1F)
            {
                // Reset Time and direction
                bl_forward = false;
                fl_start_time = Time.time;
            }
        }
        else // Move to start pos
        {
            transform.position = Vector2.Lerp(V2_end_position, V2_start_position, _fl_step);
            if (Vector2.Distance(transform.position, V2_start_position) < 0.1f)
            {
                // Reset Time
                bl_forward = true;
                fl_start_time = Time.time;
            }
        }
    }//-----
 
}//==========
