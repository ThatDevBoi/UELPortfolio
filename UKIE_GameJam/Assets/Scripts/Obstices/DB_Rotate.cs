using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Rotate : MonoBehaviour
{
    //---------------------------------------------------------------------------------------
    // Variable Definitions
    public float fl_rotation_rate = 180;

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Rotate this object about the z axis 
        transform.Rotate(0, 0, fl_rotation_rate * Time.deltaTime);
    }//-----

}//==========
