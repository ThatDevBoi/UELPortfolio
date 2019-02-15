using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_UI_WaveManager : MonoBehaviour
{

    private Text Wave;

    public int WaveNumber = 1;

	// Use this for initialization
	void Start ()
    {
        // Finding the Text Component with the tag Text
        Wave = GameObject.FindGameObjectWithTag("Wave").GetComponent<Text>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        // When the wave changes wave text + 1
        Wave.text = "Wave:" + WaveNumber;




        // IF The Player dies, reset the Number to 1
    }
}
