//---------------------------------------------------------------------------------------
// Simple 2D Screen Wrap
// David Dorrington, UEL Games, 2017 
//---------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_ScreenWrap : MonoBehaviour {

    //---------------------------------------------------------------------------------------
    // Variables
    public Rect re_screen_limit = new Rect(-10, 5, 10, -5);
    // This is a rectangle with 4 values x, y, width, height

    //---------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // constrain x axis
        if (transform.position.x < re_screen_limit.x)
            transform.position = new Vector2(re_screen_limit.width, transform.position.y);

        if (transform.position.x > re_screen_limit.width)
            transform.position = new Vector2(re_screen_limit.x, transform.position.y);

        // constrain y axis
        if (transform.position.y > re_screen_limit.y)
            transform.position = new Vector2(transform.position.x, re_screen_limit.height);

        if (transform.position.y < re_screen_limit.height)
            transform.position = new Vector2(transform.position.x, re_screen_limit.y);
    }//-----
 
}//==========