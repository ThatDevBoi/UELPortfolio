using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BegButtMove : MonoBehaviour
{
   
    public float speed = 3.0f;
    public float jumpForce = 100;
    
    [SerializeField]
    private bool grounded;
    private bool keyAlternate;


    private Rigidbody2D rb2d;
   
    private RigidbodyConstraints2D rbrot;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Freeze z rotation constraint
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
		

        Move(); // Calling the move function
        
    }

    void Move() //Movement of the PC
    {

        float X = Input.GetAxis("Horizontal") * speed * Time.smoothDeltaTime;
        float Y = Input.GetAxis("Vertical") * speed * Time.smoothDeltaTime;



        if (Input.GetKeyDown("left") && keyAlternate == false) // Pressing the left key pushes the PC forward, allowing for alternating buttons
        {
            
            keyAlternate = true;
            transform.position += Vector3.right * speed * Time.deltaTime;
            print("Move right!");


        }

        if (Input.GetKeyDown("right") && keyAlternate == true) // Pressing the right key pushes the PC forward, allowing for alternating buttons
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            keyAlternate = false;
            print("Move right!");


        }

        if (Input.GetKeyDown(KeyCode.Space)) // Allows for the PC to jump when grounded //&& !grounded
        {


        if(Input.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    rb2d.AddForce(new Vector2(0f, jumpForce));      // Using physics to oush the gameObject up using Force
                }
                else if (!grounded)
                    rb2d.velocity = (Vector2.zero);
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground") // Tag the ground to allow the player to be grounded
            grounded = true;
        else
            grounded = false;
        
    }
    

    


}
