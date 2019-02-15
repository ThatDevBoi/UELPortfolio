using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Scripts
    public Player_Manager _Player_Script;
    // Zombie Wave Manager Scripts
    public Zombie_Waves _Zombie_Script;
    public DB_Zombie_WaveManager_NO_UI _Zombie_Script_;
    // Int for Money
    static public int Money;
    public int Take_PC_Health = 1;
    // Text Component that shows Money Int
    Text Money_Text;
    // Bools
    public bool Hide_Camera;
    // GameObjects
    public GameObject Player, StartMenu; 
    private GameObject Zombie_01, Zombie_02;
    public GameObject Player_Camera, UI_Cam, GameOver_Screen, Android_Controls, Wave_Manager;

    void Start()
    {
        // Finding Zombies 
        Zombie_01 = GameObject.FindGameObjectWithTag("Enemy");
        Zombie_02 = GameObject.FindGameObjectWithTag("Enemy");
        // Wave Manager that has UI
        _Zombie_Script = GameObject.FindGameObjectWithTag("NPC_Spawner").GetComponent<Zombie_Waves>();
        // Wave Manager that doesnt monitor UI 
        _Zombie_Script_ = GameObject.FindGameObjectWithTag("NPC_Spawner_02").GetComponent<DB_Zombie_WaveManager_NO_UI>();
        // Finding the Player GameObject
        Player = GameObject.FindGameObjectWithTag("Player");
        // Finding the Player Manager script attached to the PLayer GameObject
        _Player_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();
        GameOver_Screen.SetActive(false);   // Set UI screen to false so its not seen until player death
        Money = PlayerPrefs.GetInt("Current Money");    // Static int Money is now being monitored by Player Preferences
        Money = 0;  // Makes static money equal 0 on play [Remove this when everything is done]
        // Android controls dont show to start with
        Android_Controls.SetActive(false);
    }
    void Awake()
    {
        Money_Text = GameObject.FindGameObjectWithTag("Money").GetComponent<Text>();    // Finding the Text Component with the tag Text
    }
    // Update is called once per frame
    void Update ()
    {
        Money_Text.text = Money.ToString();     // Making money text pass Variables through scenes with the int money
        PlayerPrefs.SetInt("Current Money", Money); // When the Int "Money" Goes up and the player exits play, The current int is saved
        // Functions
        Camera_Switch();
        
        // When the player has Health, The GameOver Canvas is hidden
        if(!_Player_Script.No_Health)
        {
            GameOver_Screen.SetActive(false);
        }
        // When the player has No Health, The GameOver Canvas is Displayed
        else
        {
            GameOver_Screen.SetActive(true);
        }
        // Reset Zombie Wave Manager Timers
        if(_Player_Script.Dead)
        {
            // Resetting the wave countdown when the Player Dies
            _Zombie_Script.waveCountdown = 5f;
            _Zombie_Script_.waveCountdown = 5f;
        }
    }
    public void Move_Player_To_Game()
    {
        // When the player is not dead
        if (!_Player_Script.Dead)
        {
            // Turns Wave Managers On
            Wave_Manager.SetActive(true);
            // Set the players Transform component to new values (Play Area)
            Player.transform.position = new Vector2(-983.61f, 7.45629f);
        }
        else
        {
            // Turns Wave Managers Off
            Wave_Manager.SetActive(false);
            // Player is not dead
            _Player_Script.Dead = false;
        }    
    }

    public void Move_Player_To_Menus()
    {
        // Player Transform component goes to start menu 
        Player.transform.position = new Vector2(695, 140.52f);
        // Add health so boolean doesnt go full retard
        _Player_Script.PCHealth =+ 1;
    }


    public void Camera_Switch()
    {
        // If the camera needs to switch 
        if(!Hide_Camera)
        {
            //if the Player is no longer Dead
            if (!_Player_Script.Dead)
            {
                // Cameras Switch
                Hide_Camera = true;

                // Player is not dead
                _Player_Script.Dead = false;

                // The Playerable Characters Camera is Active
                Player_Camera.SetActive(true);

                // UI Camera is No Longer Active
                UI_Cam.SetActive(false);
            }
        }       // However When the Player Dies
        else if(_Player_Script.Dead)
        {
            // Player is Dead
            _Player_Script.Dead = true;
            // Camera switches back
            Hide_Camera = false;
            // UI Camera is Active
            UI_Cam.SetActive(true);
            // Player Camera is Not Active
            Player_Camera.SetActive(false);
        } 
    } 
    // When the Player presses Play The Player Character Is No Longer Dead
    public void Respawn()
    {
        _Player_Script.Dead = false;

    }

    public void MainMenuRespawn()
    {
        _Player_Script.No_Health = false;
    }


    public void Restart_Scene(){
        // Restart the scene
        SceneManager.LoadScene("StartMenu");
    }

}
