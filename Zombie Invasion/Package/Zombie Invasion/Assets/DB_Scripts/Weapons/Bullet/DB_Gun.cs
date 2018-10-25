using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DB_Gun : MonoBehaviour
{

    // Public Variables
    public float damage = 10f;                          // How much damage our bullet will do
    public float range = 100f;                          // How far the bullet will go
    public float fireRate = 15f;                        // How fast the gun can shoot
    public Camera fpsCam;                               // Finding the camera 
    public float impactForce = 30f;                     // The force a bullet hits an object
    public int maxAmmo = 30;                            // The max ammount of ammo the gun can have 
    public Animator animator;                           // Calling the animator
    public float reloadTime = 1f;                       // How long it takes to reload


    /// Particle Effects
    public ParticleSystem muzzleFlash;                  // Calling Muzzle flash particle effect
    public GameObject impactEffect;                     // calling Impact Particle Effect

    // Audio
    public AudioClip Gun_ReloadPC;                      // Audio Clip Reload
    public AudioClip Gun_Shoot;                         // Audio Clip Shoot

    /// UI  
    Text ammo;                                          // Declaring the text object 
    const int MaxAmmo = 0;                             // Constant number will be 30 Bullets 
    int Ammunition;                                     // Number of ammo 
    string ammoDisplay;                                 // String display of /30 bullets

    /// New Ammo UI 
    /// // Canvas Ammos
    public int curAmmo;
    public int backupAmmo;
    public Text curAmmoText;
    public Text backupAmmoText;
    

    /// Private Variables
    private float nextTimeToFire = 0f;                  // The next time we have maxAmmo
    private bool isReloading = false;                   // Is the player reloading                           
    private int currentAmmo;                            // our current amount of ammo
    private AudioSource PC;                             // Sharing the audioSource


    void Start()
    {
        
        currentAmmo = maxAmmo;                                                      // Making sure we start with max ammo

        /// Ammo UI
                       
        Ammunition = maxAmmo;                                                   // Ammunition always = Max Ammo 
        backupAmmo = 90;

        /// Audio 
        PC = GetComponent<AudioSource>();
        PC.clip = Gun_ReloadPC;
        PC.clip = Gun_Shoot;
    } // ------------------------------------------------------------------------------------------------------------------------

    void OnEnable()
    {
        
        isReloading = false;                        // Bool Dont reload on the start of play
        animator.SetBool("Reloading", false);       // Dont use animation when scene starts
    } // -----------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {

        curAmmoText.text = currentAmmo.ToString();
        backupAmmoText.text = backupAmmo.ToString();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo > 0)            // When R is pressed on the KeyBoard
            StartCoroutine(Reload());                               // Start to reload <---- Using reload IEnumerator

        if (isReloading)                                             // when player returns the reload stops
            return;

        if (currentAmmo <= 0)                                       // if ammo = 0 you reload
        {
            StartCoroutine(Reload());                               // Getting reload IEnumerator
            return;                                                 //  return to shoot Gun
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Ammunition > 0 && !isReloading)               // Press Mouse Button 1 to shoot (Left Click)
        {
            nextTimeToFire = Time.time + 1f / fireRate;                                             // Showing when the player can fire calculating weapons fire rate in real time
            Shoot();                                                                                // Shoots gun void
                                             
        }
    } // ---------------------------------------------------------------------------------------------------------------------


    IEnumerator Reload()
    {
        isReloading = true;                                                                  // Player has 0 bullets player reloads
        {
            PC.clip = Gun_ReloadPC;                                                          // Find Reload script

            PC.Play();                                                                      // Play Audio Clip through audio source component
        }
        Debug.Log("Reloading...");                                          // Console shows reloading
        animator.SetBool("Reloading", true);                               // Anmation plays
        yield return new WaitForSeconds(reloadTime - .25f);               // Pause for reload time 
        animator.SetBool("Reloading", false);                            // Animation stays idle
        yield return new WaitForSeconds(.25f);                          // Wait 0.25 seconds before you can shoot after animation          
        isReloading = false;                                           // When not reloading the ammo is Max 


        var shot = maxAmmo - currentAmmo;                           // declaring a new variable called shot shot is MaxAmmo
        if (backupAmmo < shot)                                      // when backupAmmo is less then shot
        {
            currentAmmo = backupAmmo;                              // currentAmmo is equal to backupAmmo
            backupAmmo = 0;                                         // BackupAmmo = 0
        }
        else
        {
            currentAmmo += shot;                                // Current ammo = Max ammo = new shot - BackUpAmmo 
            backupAmmo -= shot;                                 // BackupAmmo = -MaxAmmo
        }
      


    } // ------------------------------------------------------------------------------------------------------------------------

    void Shoot()
    {
        muzzleFlash.Play();                                          // Calling particle component when raycast hits object
        currentAmmo--;                                              // Stating the max ammo in start void
        {
        PC.clip = Gun_Shoot;
        PC.Play();
        }
        RaycastHit hit;
       
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,  out hit, range))                // Shoot a raycast where the camera is looking
        {
            PC.clip = Gun_Shoot;
            PC.Play();
            Debug.Log(hit.transform.name);                                                                      // Showing the object that the raycast hits
            DB_Health target = hit.transform.GetComponent<DB_Health>();                                         // calling the enemy health refernce script to kill the enemy Then damage can be shown

            if (target != null)                                                                                // Do we hit the target if yes take damage, if no nothing happens
            {
                target.TakeDamage(damage);
            }
  
            if (hit.rigidbody != null)                                                              // Has the players raycast hit an object with a rigidbody component
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);                               // Object forces back depending on int
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    } // --------------------------------------------------------------------------------------------------------------------------
} // =============================================================================================================================================================