using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PM_LoadSameScene : MonoBehaviour
{
    public string st_scene_to_load;


    // ----------------------------
    public void LoadNewScene()
    {
    

        SceneManager.LoadScene(st_scene_to_load);
    } //-----------

    // -------------------------------------
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } //---------

}
