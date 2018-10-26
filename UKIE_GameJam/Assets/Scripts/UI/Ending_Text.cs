using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ending_Text : MonoBehaviour
{
    public string[] string_endScreen;
    public string var;// string of different texts that will appear

    public Text End_Screen_Text;        // Text Component

    public bool Dies_Young, Dies_Adult, Dies_OldMan;        // Boolean FLAGS
	// Use this for initialization
	void Start () {
        End_Screen_Text = GetComponent<Text>();

        if (GM.Age <= 24)
        {
            Dies_Young = true;
            string_endScreen[0] = ("You lived for a short amount of time on this adventure we call life... You will be remembered fondly for your ability to smile despite not knowing what's going on.");
        }
        else
            Dies_Young = false;

        if (GM.Age <= 45)
        {
            Dies_Adult = true;
            string_endScreen[1] = ("You managed to reach your adulthood on this journey we call life. You will be remembered for your terrible haircut at school and your belief to always try your best.");
        }
        else
            Dies_Adult = false;

        if (GM.Age <= 76)
        {
            Dies_OldMan = true;
            string_endScreen[2] = ("You survived all these years and entered elderhood on this challenge we call life. You will be remembered often for your compassion, which is a rare and wonderful thing and your heart, which gave so much, even when it gave out.");
        }
        else
            Dies_OldMan = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Dies_Young)
        {
            End_Screen_Text.text = string_endScreen[0];
        }
        if(Dies_Adult)
        {
            End_Screen_Text.text = string_endScreen[1];
        }
        if (Dies_OldMan)
        {
            End_Screen_Text.text = string_endScreen[2];
        }
    }
}
