using UnityEngine;
using UnityEngine.UI;

public class DB_WeaponSwitching : MonoBehaviour
{
    // Weapon 0 will be selected first
    public int selectedWeapon = 0;
 

    Text ammo;
    int Ammunition;
    string ammoDisplay;


    private int currentAmmo;

    // Use this for initialization
    void Start()
    {
        SelectWeapon();
        GetComponent<DB_Gun>();
        //ammo = GameObject.Find("HitBox/BulletCount").GetComponent<Text>();
        Ammunition = currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;        // new Variable previousSelectedWeapon equals to the public int Selected Weapon

        // Does the player use scrool wheel up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // Yes so add one and change weapon from 0 to 1 
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
                selectedWeapon++;           // Add an int to scroll through childed gameObjects 
        }
        // Does the player use the scrool wheel down 
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Yes so subtract from 1 to go back to 0
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
                selectedWeapon--;               // Declines int and goes back to previous weapon
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;                                     // Reload to max ammo player continues
            ammo.text = currentAmmo.ToString() + ammoDisplay;
        }


        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }
    void SelectWeapon()
    {
        // Default weapon number 0
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                // Select a weapon from 1 - 2 
                weapon.gameObject.SetActive(true);
            else
                // if not stay on current weapon
                weapon.gameObject.SetActive(false);
            // Increaing number in weapon wheel
            i++;
        }
    }
  



}
