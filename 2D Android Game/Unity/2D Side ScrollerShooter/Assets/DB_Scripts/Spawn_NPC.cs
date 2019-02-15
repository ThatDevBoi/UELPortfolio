// Spawner 2D script 
// David Peter Brooks
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_NPC : MonoBehaviour
{
    // NPC Zombies Arrayed Reference 
    public GameObject [] Enemy;

    // How long it takes for the enemies to spawn 
    //public float spawnTime = 3f;

    // References in Array Where the spawn locations are
    public Transform[] spawnPoints;

    // public GameObject[] Zombies;

    

    // Count/Amount of enemies that get spawned
    public int Count;

    // Countdown that'll start the wave
    public float waveCountdown;

    // Float countdown timer to find any enemies 
    private float searchCountDown = 1f;

    // Int of the next wave
    public int NextWave;

    // The time period it takes to start a new wave
    public float Time_Between_Waves = 5f;

	// Use this for initialization
	void Start ()
    {
        // When the scene starts the Spawn Function is called and references spawn time. Objects know to wait 5 seconds 
        // InvokeRepeating("Spawn", spawnTime, spawnTime);


        // The countdown always stays at 5
        waveCountdown = Time_Between_Waves;
        if(spawnPoints.Length == 0)
        {
            Debug.Log("ERROR no spawn Points");
        }
	}

    void Update()
    {
      if(!EnemyIsAlive())
        {
            WaveCompleted();
        }
        else
        {
            return;
        }

      if(waveCountdown <= 0)
        {
            SpawnEnemy();
        }
      else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    public void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        // waveCountdown = Next Time Wave Starts Again
        waveCountdown = Time_Between_Waves;
        if(NextWave + 1 > NextWave)
        {
            if (Count <= 0)
            {
               // GameManager.waveCount += NextWave;
            }
        }
        
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;


        if(searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnEnemy()
    {
        // Random chance for Enemy gameObject to spawn with choosen locations 
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        // Calculating how many enemies theyre;
        for(int i = 0; i < Count; i++)
        {
            // Spawn Random Enemies within random points in the scene.
            Instantiate(Enemy[Random.Range(0, Enemy.Length)], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
