using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_EnemyTriggerZone : MonoBehaviour
{
    public BoxCollider2D LeftCollider;

    public BoxCollider2D RightCollider;

    public float Timer = 1;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (RightCollider.enabled == false)
            Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            Timer = 1;
            RightCollider.enabled = true;
        }

        if (LeftCollider.enabled == false)
            Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            Timer = 1;
            LeftCollider.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            RightCollider.enabled = false;
        }
        if(other.gameObject.tag == "Enemy")
        {
            LeftCollider.enabled = false;
        }
    }

    
}
