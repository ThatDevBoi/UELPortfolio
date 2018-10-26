using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathwall : MonoBehaviour {
    private float fl_speed;
    private float start_time;
    // Use this for initialization
    void Start () {
        start_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        fl_speed = (Mathf.Exp((Time.time-start_time) * 0.035f) * 0.01f);

        //fl_speed += fl_playerspeed + adaptive_speed + Time.deltaTime;



        gameObject.transform.Translate(Vector3.right * fl_speed, Space.World);
    }
}
