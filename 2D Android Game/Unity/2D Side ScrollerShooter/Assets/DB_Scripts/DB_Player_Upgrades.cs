using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Player_Upgrades : MonoBehaviour
{
    // Reference to Player GameObject
    GameObject PlayerCharacter;
 
    // Upgrade Ammo Button
    public Button ammoUpgrade;

    // Int added when player has enough money to make a purchase
    public static int BulletAdd = 2;


     static public int Ammo_Charge = 250;


    static bool Created_Object;

    // Reference to Player Script
    private Player_Manager _PlayerScript;


    // Bools for purchases
    public bool Ammo_Amount_01, Ammo_Amount_02, Ammo_Amount_03, Ammo_Amount_04, Ammo_Amount_05, Ammo_Amount_06, Ammo_Amount_07, Ammo_Amount_08;

    public Text Ammo_Text, Damage_Text, Melee_Text;

    void Awake()
    {
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        // Find The Player and script
        _PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();

        // Find the Button
        ammoUpgrade = GameObject.FindGameObjectWithTag("Ammo_Button").GetComponent<Button>();

    }

    // Use this for initialization
    void Start ()
    {

        if(!Created_Object)
        {
            DontDestroyOnLoad(gameObject);
            Created_Object = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
       
        //Ammo_Text = GameObject.FindGameObjectWithTag("Ammo_Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Ammo_Text.text = "$" + Ammo_Charge;
    }

    public void Upgrade_Ammo_Amount() 
    {

        if(!Ammo_Amount_01)
        {
            if (GameManager.Money >= 250)
            {
                Ammo_Amount_01 = true;
                _PlayerScript.GetComponent<Player_Manager>().MaxAmmo += BulletAdd;
                _PlayerScript.GetComponent<Player_Manager>().Bullets = 6;
                GameManager.Money -= 250;
                Ammo_Charge += 250;
                Ammo_Amount_02 = true;
            }
            else
                Ammo_Amount_01 = false; 
        }
        // When ammo 01 is true and the required money is needed & if Ammo_Amount = true. Buy Goods
            if (!Ammo_Amount_02 && GameManager.Money >= 500)
            {
                Ammo_Amount_02 = true;
                // Make Max ammo go up by the bullets were adding
                _PlayerScript.GetComponent<Player_Manager>().MaxAmmo += BulletAdd;
                // Makes bullets = 8
                _PlayerScript.GetComponent<Player_Manager>().Bullets = 8;
                // Take away money spent
                GameManager.Money -= 500;
                // Make purchase cost go up by 250
                Ammo_Charge += 250;
            }
            else
                Ammo_Amount_02 = false;

    }

    public void Upgrade_Melee_Damage()
    {

    }

    public void Upgrade_Bullet_Damage()
    {

    }

   
}
