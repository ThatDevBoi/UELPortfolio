using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Manager : MonoBehaviour
{
    // Android Movement 
    // Variables to contain move direction in the X axis
    float dirX;
    // Variables for move speed and jump force adjustable in inspector 
    public float moveSpeed = 5f, jumpForce = 700f;

    // Keyboard Movement
    public float speed = 6f;

    public float jumpSpeed = 8f;

    private float movement;
    // How fast the player can shoot
    public float FireRate = 3f;
    // Decides the next time to fire a bullet
    public float NextShot = 1f;
    // Amount of bullets player has
    public int Bullets;
    // Int for Reload Bullets
    public int MaxAmmo = 6;
    // Time it takes to reload
    public float reloadTime = 1.5f;

    // If the player is jumping in the Y axis // Is the player Facing right
    public bool facingRight, Jumping, IsReloading, damage, Dead, No_Health;

    // Components
    // Reference to rigidbody2D component
    Rigidbody2D rb;
    // Referencing the Animator 
    public Animator Animator;

    //Shooting Gun
    // Refernece to 2 bullet gameObjects editable in inspector
    public GameObject leftBullet, rightBullet;
    // Reference of Fire position transform
    Transform Firepos;
    // Amount that we will use to hurt the player
    public float Damage;
    // Amount of health the player has
    public float PCHealth;
    // CircleCollider2D reference on child GameObject
    public CircleCollider2D Collid;


    private static bool DelDup = false;

    // Use this for initialization
    void Start ()
    {
        //----------------------------------------------------------------------------------

       // AddBullets_Charge = 250;

        //AddBullets_Charge = PlayerPrefs.GetInt("New Charge");


        // Makes sure that 2 player GameObjects dont get made, only keeos 1 
        if (!DelDup)
        {
            DontDestroyOnLoad(gameObject);
            DelDup = true;
        }
        else
        {
            Destroy(this.gameObject);
        }



        

        // Getting Rigidbody2D component to operate it 
        rb = GetComponent<Rigidbody2D>();

        // Finding the Animator component
        Animator = GetComponent<Animator>();

        // Where the gameObject Left/Right bullets Instantiate from;
        Firepos = transform.Find("FirePoint");

        // Start scene with 6 bullets
        Bullets = 6;

        // RBullets(ReloadBullets) will add th=o bullets
        Bullets = MaxAmmo;

        // Bools
        // The player is on the ground
        Jumping = false;
        // At the start bool facingRight is true. PC always sart facing right 
        facingRight = true;
        // The player starts with 6 bullets and doesnt need to reload
        IsReloading = false;
        // Dead = true on start
        Dead = true;

        No_Health = false;
        //-------------------------------------------------------------------
    }//==========
	
	// Update is called once per frame
	void Update ()
    {
        //-------------------------------------------------------------------



        

        // Checks in the game state that the text value = x number, triggers boolean so the shop doesnt glitch

        
        /*if(AddBullets_Charge >= 500)
        {
            Ammo_Amount_01 = true;

        }

        if(AddBullets_Charge >= 750)
        {
            Ammo_Amount_02 = true;
        }*/
        
        //PlayerPrefs.SetInt("New Charge", AddBullets_Charge);




        DontDestroyOnLoad(gameObject);
        // Kill Switch
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PC_Dead();
        }

        // Functions
        Flip();
        KeyBoardmovement();
        Androidmove();
        PlayerDying();
        // Change to shoot with button use jump button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        //-----------------------------------------------------------------
    }//==================================
    void FixedUpdate()
    {
        //----------------------------------------------------------------

        // Pass a velocity to Rigidbody2D component according to dirx value multiplied by move speed
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        // For Keybaord 
        //rb.velocity = new Vector2(movement * speed, rb.velocity.y);

        //----------------------------------------------------------------
    }//=============
    void KeyBoardmovement()
    {
        //---------------------------------------------------------------

        // Getting the X asix 
        movement = Input.GetAxis("Horizontal");
        // If the movement is less than 0 use the rigidbody2d component to add speed along the x axis
            if(movement > 0f)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
         else if (movement < 0f)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
            else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
            // If the up Arrow Key is pressed, Jumping boolean is true and player jumps of the ground using the Vecotr 2 and jumpspeed 
            if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Rigidbody2D component pushes the player up on the Y axis with speed
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
            //------------------------------------------------------------
    } //=========
    void Androidmove()
    {
        //----------------------------------------------------------------

            // Getting dirx value when any of UI move buttons are pressed(For android)
            dirX = CrossPlatformInputManager.GetAxis("Horizontal");

            Animator.SetFloat("Speed", Mathf.Abs(dirX));

        // If jump button is pressed then DoJump method is invoked (For android)
        if (CrossPlatformInputManager.GetButtonDown("Jump")) 
        {
            DoJump();
            Animator.SetBool("Jump", true);
        }
        else
        {
            Animator.SetBool("Jump", false);
        }

        if (CrossPlatformInputManager.GetButtonDown("Shoot"))
        {
            // If the bullets are equal to max ammo and Bullets = 0
            if(Bullets < MaxAmmo && Bullets <= 0)
            {
                // Starts to reload
                IsReloading = true;
            }
        }
       
        if(IsReloading == false)
        {
            if (CrossPlatformInputManager.GetButtonDown("Shoot") && Time.time > NextShot)
            {
                NextShot = Time.time + FireRate;

                Animator.SetBool("IsShooting", true);

                Bullets--;
                Fire();
            }
            else
            {
                Animator.SetBool("IsShooting", false);
            }
        }

        if(IsReloading == true)
        {
            Animator.SetBool("IsShooting", false);
            reloadTime -= Time.deltaTime;
            if(reloadTime <= 0)
            {
                Bullets = MaxAmmo;
                IsReloading = false;
                reloadTime = 1.5f;
            }
        }
       
            if (CrossPlatformInputManager.GetButtonDown("Melee"))
            {

            Collid.enabled = !Collid.enabled;

            Animator.SetBool("Melee", true);
            }
            else if (CrossPlatformInputManager.GetButtonUp("Melee"))
            {

                Animator.SetBool("Melee", false);

                Collid.enabled = false;

            }
        //---------------------------------------------------------------------
    } //=====

    

    public void DoJump()
    {
        //----------------------------------------------------------------------

        // Simple check to not allow the PC to jump while in the air
        if (rb.velocity.y == 0)
        // Add force to Rigidbody2D component in Y axis Direction 
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);

        //----------------------------------------------------------------------
    } //=======
    
    void Fire()
    {
        //----------------------------------------------------------------------

        if(Jumping == false)
        {
            if (facingRight)
            {
                Instantiate(rightBullet, Firepos.position, Quaternion.identity);
            }
            if(!facingRight)
            {
                Instantiate(leftBullet, Firepos.position, Quaternion.identity);
            }
        }
        else
        {
            Jumping = true;
        }
        //---------------------------------------------------------------
    }//==============
    void Flip()
    {
        //---------------------------------------------------------------

        // If The Player Is or Isnt Facing Right Local Scale -1 If FacingRight = False using the transform of the gameObject to change the facing position

        if (movement > 0 && !facingRight || movement < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }

        if(dirX > 0 && !facingRight || dirX < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        } //----------------------------------------------------------
    } //========
    // Before the player dies
    void PlayerDying()
    {
        //------------------------------------------------------------

        if(PCHealth <= 0)
        {
            //Debug.Log("Dead");

            PC_Dead();
            
        }
        //------------------------------------------------------------
    } //==========

    // When the player is dead
    public void PC_Dead()
    { 
        //-----------------------------------------------------------
        if(PCHealth <= 0)
        {

            Dead = true;

            No_Health = true;

            // Play the Animation 

            // Make the Screen Fade Out 
        }
        else 
        {
            Dead = false;

            No_Health = false;
        }
        //-------------------------------------------
    }//===========



   









    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Jumping = false;
        }
        else
        {
            Jumping = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Jumping = true;
        }
        else
        {
            Jumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyMeleeTrigger")
        {
            damage = true;
            //Debug.Log("Taking Damage");
            PCHealth -= Damage;
        }
    }
    

}
