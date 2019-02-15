using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_Waves : MonoBehaviour
{
    // Different states that the wave spawner will need
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    // Allowes me to change the properties values in the inspector
    [System.Serializable]
    public class Wave
    {
        // Name of the wave reference 
        public string name;
        // Reference to the enemy prefab that will be Instaniated
        public Transform enemy;
        // Count/Amount of enemies that get spawned
        public int count;
        // How fast the enemy prefabs spawn
        public float Spawnrate;
    }
    // Array that states how many waves that can be created in the inspector
    public Wave[] waves;
    // Points where the enemies will be instaniated from
    public Transform[] spawnPoints;
    // Reference to Male Zombie GameObject
    public GameObject ZombieM;
    // Reference to my NPC script
    private DB_Simple_NPC_Chase _enemyScript;

    

    public int nextWave;
    // The time period it takes to start a new wave 
    public float time_between_waves = 5f;
    // Countdown that'll start the wave
    public float waveCountdown;
    // Float cpountdown timer 
    private float searchCountDown = 1f;
    // Spawn state of enemy 
    private SpawnState state = SpawnState.COUNTING;

    public GameObject WM;

    private DB_UI_WaveManager _WM_Script;

    // Reference to Player Script
    public Player_Manager _Player_Script;

    //
    public int WaveRound = 1;

    public static int Enemies = 0;


    void Start()
    {
        // Finding Payer Manager Script
        _Player_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();

        _WM_Script = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<DB_UI_WaveManager>();

       // _GM_Script = GM.GetComponent<GameManager>();

        // The countdown always starts at 5 seconds
        waveCountdown = time_between_waves;
        if (spawnPoints.Length == 0)
        {
            Debug.Log("ERROR no spawn Points");
        }
        // Referencing NPC Script on Enemy GameObject
        // _enemyScript = ZombieM.GetComponent<DB_Simple_NPC_Chase>();
        
    }

    void Update()
    {
        Debug.Log(EnemyIsAlive());
           
        if (state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                // Begin a new wave
                WaveCompleted();

            }
            else
            {
                return;
            }
        }

        // If the Player is Alive
        if(!_Player_Script.Dead)
        {
            waveCountdown -= Time.deltaTime;
            // If the coundown is 0 engine starts to spawn objects.
            if (waveCountdown <= 0)
            {
                // Checking if we have started spawning 
                if (state != SpawnState.SPAWNING)
                {
                    // Use spawning method in the IEnumerator
                    // Start Spawning Enemies
                    StartCoroutine(SpawnWave(waves[nextWave]));
                    //Debug.Log("IT STARTED");
                }
            }
        }
        // If the Countdown timer is not at 0 yet we use Time to decrease the Cooldown value
       // else if(_Player_Script.Dead)       // If the player is dead
        //{
            // Subtract per fram until Countdown <= 0
            //waveCountdown -= Time.deltaTime;
       // }



    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        // Counting down the countdown to start the next wave so state = Spawning
        state = SpawnState.COUNTING;
        // waveCountdown = next time wave starts again
        waveCountdown = time_between_waves;
        // if the next wave is bigger than the number or waves i have 
        if(nextWave + 1 > waves.Length - 1)
        {
            // Loop Waves and go back to first wave
            nextWave = 0;
            Debug.Log("Waves all completed LOOPING");
        }
        else
        {
            // When the round is completed it increases to the next round by 1 
            nextWave++;

            // Making the GameManager wave int = WaveRound;
            WM.GetComponent<DB_UI_WaveManager>().WaveNumber += WaveRound;

            // Waves Compltes Add 1 to Wave Int;
            WaveRound++;

            
            // When Wave 2 Begins Enemy Health = 2
            // ZombieM.GetComponent<DB_Simple_NPC_Chase>().Health++;
        }  
    }
    bool EnemyIsAlive()
    {
        // Start the countdown by subtracting with time
        searchCountDown -= Time.deltaTime;
        // && if the searchCountDown is more than or equal to 0 we start searching for the GameObjects 
        if(searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                // Return false if no enemies are found. All enemies are dead
                return false;
            }
        }
        // Enemies are found and still active in the scene
        return true;

    }
    
    // IEnumerator means it does a method and waits before continuing
    IEnumerator SpawnWave(Wave _wave)
    {
        
        //Debug.Log("Spawning Wave" + _wave.name);
        // Ready to spawn enemies 
        state = SpawnState.SPAWNING;
        // Calculating how many enemies the engine needs to spawn using Count 
        for (int i = 0; i < _wave.count; i++)
        {
            // Spawn enemies for that wave
            SpawnEnemy(_wave.enemy);
            // Wait a certain amount of time by dividing 1 / Spawnrate so enemies spawn at a resonable rate until theyre all dead and the loop starts to the next wave
            yield return new WaitForSeconds(1f / _wave.Spawnrate);
        }
        

        // Waiting for the player to kill the current waves enemies or waiting to see if the player survives 
        state = SpawnState.WAITING;

        // Returns nothing to the IEnumerator so it knows the method is complepte 
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy:" + _enemy.name);

        // Spawn points engine decides at random which ones to choose and spawns GameObject prefab
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        Enemies++;
        //Debug.Log(Enemies);
        
      


        
        
    }
}
