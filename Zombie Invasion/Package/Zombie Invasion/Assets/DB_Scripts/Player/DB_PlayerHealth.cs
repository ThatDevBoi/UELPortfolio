using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DB_PlayerHealth : MonoBehaviour
{
    // Players health can go to 100 at its max
    public const int maxHealth = 100;
    // The current Health of the player is the max health that the player can have, also recording the damage taken from enemies
    public int currentHealth = maxHealth;
    // locating the players Health Bar
    public RectTransform healthBar;


    public void TakeDamage(int amount)
    {
        // Current Health = the amount of health the player has in that instance 
        currentHealth -= amount;
        // If the player has 0 health
        if (currentHealth <=0)
        {
            // Player dies
            currentHealth = 0;
            Debug.Log("Dead");
        }
        // Helping the Player find the Health bar
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
