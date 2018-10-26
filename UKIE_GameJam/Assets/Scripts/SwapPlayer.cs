using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPlayer : MonoBehaviour {

    public GameObject newPlayer; //Place on trigger object

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    void OnTriggerEnter2D(Collider2D cld)
    {
        if (cld.gameObject.tag == "Player")  // Locate the "Player" tag to denote collision
        {
            

            //print(cld.gameObject);
            cld.gameObject.SetActive(false); // Deactivates the current PC
            Instantiate(newPlayer, transform.position, transform.rotation); // Instansiat the new player you set
            this.gameObject.SetActive(false); // Deactivates the trigger object
        }


    }
}
