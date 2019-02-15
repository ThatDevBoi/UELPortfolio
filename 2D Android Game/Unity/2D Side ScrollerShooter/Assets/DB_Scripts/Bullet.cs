using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variabes bullet Movement
    // x/y axis reference || editable in inspetor
    public Vector2 speed;
    // Reference Rigidbody2D component
    private Rigidbody2D rb;


    // Use this for initialization
    void Start()
    {
        // Finding Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Rigidbody always moves object with speed and velocity
        rb.velocity = speed;
    }

    public void OnBecameVisible()
    {
        // When the Bullet GameObject enters the scene it moves with speed 
        Debug.Log("HE ALIVE");
        rb.velocity = speed;
    }

    public void OnBecameInvisible()
    {
        // When the bullet prefab is no longer in screen view we destroy the object
        Debug.Log("HE DEAD");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the gameObject meets collsion with a GameObject called Enemy gameObject destroys
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
