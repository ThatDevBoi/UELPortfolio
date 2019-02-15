using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Controler : MonoBehaviour
{
    // NPC Movment Speed 
    public float Speed;

    // Where the NPC will go
    public Transform Target;

    // Rigidbody2D Component Reference
    public Rigidbody2D RB_NPC;

    public Animator Anim;

    public bool Facing_Right;

    // Health Value
    public float Health = 1;

    // Damage Value
    public float Damage;

    // Damage from melee attacks value
    public float MeleeDamage;

    // Money gained from killing enemies 
    public int ScoreValue = 2;

    // Use this for initialization
    void Start ()
    {
        Anim = GetComponent<Animator>();

        RB_NPC = GetComponent<Rigidbody2D>();

        Target = GameObject.FindGameObjectWithTag("RespawnPoint_02").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Dead();
        Move_NPC();

        if (Target.position.x > transform.position.x)
        {
            // Face right
            Facing_Right = true;
            // Flip Right
            transform.localScale = new Vector3(1, 1, 1);
            //Anim.SetFloat("Speed", (speed));
        }
        // otherwise // If the PC position is Less than the current position then face left 
        else if (Target.position.x < transform.position.x)
        {
            // Face left
            Facing_Right = false;
            // Flip Left
            transform.localScale = new Vector3(-1, 1, 1);
            //Anim.SetFloat("Speed", (speed));
        }

    }


    public void Dead()
    {
        if(Health <= 0)
        {
            Zombie_Waves.Enemies--;

            Debug.Log(Zombie_Waves.Enemies);

            GameManager.Money += ScoreValue;

            Destroy(gameObject);
        }
    }

    public void Move_NPC()
    {
        // Moves NPC positive moving left
        transform.Translate(Speed * Time.deltaTime, 0, 0);

    }

    public void Respawn()
    {
        // Bool is true

        // GameObject Respawns

        // What happens when we repsawn

        //Add respawned GameObjects to Spawner Count
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerMeleeTrigger")
        {
            Debug.Log("HitEnemy");
            Health -= MeleeDamage;
        }
        if (other.gameObject.tag == "Bullet")
        {
            Health -= Damage;
        }
        if(other.gameObject.tag == "RespawnPoint_02")
        {
            Debug.Log("Zombie Dead");
            Destroy(gameObject);
            Zombie_Waves.Enemies--;
            Debug.Log(Zombie_Waves.Enemies);
            Respawn();
        }
    }
}
