using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_AdvanceGun : MonoBehaviour
{

    // How much damage our bullet will do
    public float damage = 10f;
    // How far the bullet will go
    public float range = 100f;
    // How fast the gun can shoot
    public float fireRate = 15f;
    // Finding the camera 
    public Camera fpsCam;
    // The force a bullet hits an object
    public float impactForce = 30f;
    // The max ammount of ammo the gun can have 
    public int maxAmmo = 30;
    // Calling the animator
    public Animator animator;
    //How long it takes to reload
    public float reloadTime = 1f;
    // Updated every frame
    float timer;
    // Calling Muzzle flash particle effect
    public ParticleSystem muzzleFlash;
    // calling Impact Particle Effect
    public GameObject impactEffect;
    // Plays audio 
    public float AudioClip;
    // Declaring the text object 
    Text ammo;
    // Constant number will be 30 Bullets 
    const int MaxAmmo = 30;
    // Number of ammo 
    int Ammunition;
    // String display of /30 bullets
    string ammoDisplay;
    // The next time we have maxAmmo
    private float nextTimeToFire = 0f;
    // Is the player reloading
    private bool isReloading = false;
    // our current amount of ammo
    private int currentAmmo;


    void Start()
    {
        // Making sure we start with max ammo
        currentAmmo = maxAmmo;
        // Finding Text component on canvas 
        ammo = GameObject.Find("HitBox/BulletCount").GetComponent<Text>();
        // Ammunition always = Max Ammo 
        Ammunition = currentAmmo;
        // Always displays ammo which is carried ammo. 
        ammoDisplay = "/ 30";
        ammo.text = currentAmmo.ToString() + ammoDisplay;
    }

    void OnEnable()
    {

        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());


        // when player returns the reload stops
        if (isReloading)
            return;

        // if ammo = 0 you reload
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Press Mouse Button 1 to shoot 
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Ammunition > 0)
        {

            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            {
                AudioSource Shoot = GetComponent<AudioSource>();
                Shoot.Play();
            }



            // Declaring ammo starts at max ammo and changes to new displayed ammo
            ammo.text = currentAmmo.ToString() + ammoDisplay;
            timer = 0;
        }
    }


    IEnumerator Reload()
    {
        // Player has 0 bullets player reloads
        isReloading = true;
        // Console shows reloading 
        Debug.Log("Reloading...");
        // Anmation plays
        animator.SetBool("Reloading", true);

        // Pause for reload time 
        yield return new WaitForSeconds(reloadTime - .25f);
        // Animation stays idle
        animator.SetBool("Reloading", false);
        // Wait 0.25 seconds before you can shoot after animation
        yield return new WaitForSeconds(.25f);
        // Reload to max ammo player continues
        currentAmmo = maxAmmo;
        ammo.text = currentAmmo.ToString() + ammoDisplay;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        // Stating the max ammo in start void
        currentAmmo--;

        RaycastHit hit;
        // Shoot a raycast where the camera is looking
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            // calling the enemy health refernce script to kill the enemy
            DB_Health target = hit.transform.GetComponent<DB_Health>();
            // Do we hit the target if yes take damage, if no nothing happens
            if (target != null)
            {
                // Take 10 damage
                target.TakeDamage(damage);
            }
            // Has the player hit an object with a rigidbody
            if (hit.rigidbody != null)
            {
                // Object forces back depending on int
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

        }
    }
}