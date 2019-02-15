using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_StartGame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
     
	}

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void Transition_To_StartMenu(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName);

        SceneManager.LoadScene(_SceneName);
    }


    public void OnApplicationQuit()
    {
        Debug.Log("Application Ended");

        Application.Quit();
    }



}
