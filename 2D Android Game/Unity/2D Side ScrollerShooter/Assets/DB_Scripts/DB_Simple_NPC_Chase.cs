using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Simple_NPC_Chase : MonoBehaviour
{
    // Movement Varibles 
    public float speed;
    public Transform PC;
    private Rigidbody2D rb_NPC;
    public bool facingRight;

    public float MinDist = 1;

    public float MaxDist = 3;

    public GameObject MaleZombie;


    public float Timer;

    public float Health;

    public float Damage;

    public float MeleeDamage;

    public Animator Anim;

    public int ScoreValue = 2;



    void Awake()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        // Activating Rigidbody2D Component attached to NPC gameObject
        rb_NPC = GetComponent<Rigidbody2D>();

       // Money = GameObject.FindGameObjectWithTag("Money").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move Function 
        Move_NPC();
        // If the PC position is more than the current position then face left
        if (PC.position.x > transform.position.x)
        {
            // Face right
            facingRight = true;
            // Flip Right
            transform.localScale = new Vector3(1, 1, 1);
            Anim.SetFloat("Speed",(speed));
        }
        // otherwise // If the PC position is Less than the current position then face left 
        else if (PC.position.x < transform.position.x)
        {
            // Face left
            facingRight = false;
            // Flip Left
            transform.localScale = new Vector3(-1, 1, 1);
            Anim.SetFloat("Speed",(speed));
        }
        if(Health <= 0)
        {
            // Score should display on money text, adds 2 everytime an enemy dies
            GameManager.Money += ScoreValue;
            // Calls dead function
            Dead();
        }
	}

    public void Dead()
    {
        
        // Destroys after 2 Seconds
        Destroy(gameObject);
    }

    public void Move_NPC()
    {
        Vector2 Distance = PC.position - transform.position;

       // if (Distance.magnitude > MinDist * MaxDist)
       // {
            // Move towards the PC gameObject Without the Rigidbody 2D component
            transform.position = Vector2.MoveTowards(transform.position, PC.position, speed * Time.deltaTime);
        Anim.SetFloat("Walking", (speed));
        // }
        // else
        // {
        //Anim.SetFloat("Walking", (speed));
      //  }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "PlayerMeleeTrigger")
        {
            Debug.Log("HitEnemy");
            Health -= MeleeDamage;
        }
        if (other.gameObject.tag == "Bullet")
        {
            Health -= Damage;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            // ignores the CircleCollider on all enemy objects so they can pass through eachother
            Physics2D.IgnoreCollision(MaleZombie.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    // Attack 


    // Spawn in a spawner


    // Death ---- Get Points from each zombie death ---- Get Money from each zombie death
}
