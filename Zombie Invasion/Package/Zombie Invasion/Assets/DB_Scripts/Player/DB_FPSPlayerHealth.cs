using UnityEngine;
using UnityEngine.UI;

public class DB_FPSPlayerHealth : MonoBehaviour
{
    // Variables For Health
    // Finding the healthbar slider 
    public Slider HealthBar;
    // fixed health Max health
    public float Health = 100;
    // The current health of the player 
    private float _currentHealth;


	// Use this for initialization
	void Start ()
    {
        // Player starts with 100 Health and the start of a scene 
        _currentHealth = Health;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            _currentHealth -= -10;
    }

    public void TakeDamage(float damage)
    {
        // Current health - damage = Health bars current Health
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;
    }
}
