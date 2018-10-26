using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DD_Load_Scene : MonoBehaviour
{

    public string st_scene_to_load;

    private AudioSource as_canvas;
    public AudioClip ac_click;
    private bool bl_load;
    public float fl_delay = 1;
    private float fl_load_time;

    private void Start()
    {
        as_canvas = GetComponent<AudioSource>();

    }


    private void Update()
    {
        if (bl_load && fl_load_time < Time.time)
        {
            SceneManager.LoadScene(st_scene_to_load);

        }
    }

    // ----------------------------
    public void LoadNewScene()
    {

        as_canvas.clip = ac_click;
        as_canvas.Play();

        bl_load = true;
        fl_load_time = Time.time + fl_delay;

    } //-----------

    // -------------------------------------
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } //---------

} //===================
