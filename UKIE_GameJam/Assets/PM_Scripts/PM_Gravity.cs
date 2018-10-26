using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_Gravity : MonoBehaviour
{

    public Rigidbody2D RB_2D;
    public bool isnegative;
    // Use this for initialization
    void Start()
    {
        RB_2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // when do you want to change the gravity.
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (RB_2D.gravityScale > 0)// if gravity is positive
            {
                isnegative = false;// im positive
            }

            if (RB_2D.gravityScale < 0)// if gravity is negavite
            {
                isnegative = true;// im negative
            }

            if (!isnegative)// if im positive
            {

                print("gravity is: " + RB_2D.gravityScale);
                RB_2D.gravityScale = -1; //im negative

            }
            if (isnegative)// if negative
            {
                print("gravity is: " + RB_2D.gravityScale);
                RB_2D.gravityScale = 1; // im positive 
            }
        }
    }
}
