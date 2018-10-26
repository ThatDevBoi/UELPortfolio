using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour {
    private GameObject newPlayer; //Place on trigger object
    public GameObject wall;          // Use this for initialization
    public float fl_speed;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D cld)
    {
        
        if (cld.gameObject.tag == "Player")  // Locate the "Player" tag to denote collision
        {
            
            wall.SetActive(true);


            //mMove.MovePC();


            //// float _fl_dist_travelled = (Tmcaime.time - fl_start_time) * Mathf.Pow(fl_speed, tTimePow);

            // //float tHeight = Camera.main.orthographicSize;//gathering height from the camera
            //// float tWidth = Camera.main.aspect * tHeight;// using height to deduce width from height)
            // float tConst;
            // tConst = Time.deltaTime;
            // if (fl_playerspeed < 0.001f)
            // {

            //     fl_speed += fl_playerspeed + adaptive_speed + Time.deltaTime;

            //     adaptive_speed = Mathf.Abs(fl_playerspeed * Mathf.Exp(Time.deltaTime) * 0.000001f);
            //     Time.timeScale = 0.5f;
            // }
            // else
            // {
            //     Time.timeScale =1;

            
        }

       

    }
}
