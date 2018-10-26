using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_Scene : MonoBehaviour
{
    #region Scene Variables
    protected string st_change_scene=("End_Menu");      // String for change scene
    #endregion

    #region Fade Out Variables
    public Texture2D fadeOutTexture;        // The Texture that will overlay the screen this can be a black image or a loading graphic
    public float fadeSpeed = 5f;        // The Fading Speed

    public int drawDepth = -1000;      // the texture order in the draw hierarchy a low number means in renders on top
    public float alpha = 1.0f;     // the texture alpha value between 0 and 1
    public int fadeDir = -1;       // the direction to fade in = -1 or out = 1
    #endregion

    #region Switch Scenes
    public void Switch_Scenes(string st_Scene_change)
    {
        SceneManager.LoadScene(st_Scene_change);      // Change the scene with whatever scene is written into the string (NameSpace)
    }
    #endregion

    #region Quit Game
    public void Quit_Game()
    {
        Application.Quit();     // Quits gme when Button is clicked

        Debug.Log("Quit Game");     // Notifies inside the console
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;      // Fade in/out the alpha value using  direction, a speed and time.delta to convert the operation to seconds

        alpha = Mathf.Clamp01(alpha);       // Force (clamp) the number between 0 and 1 because GUI color uses alpha vlues getween 0 and 1 
        // set color of our GUI (in this case our Texture).All color values remain the same & the Apla is set to the alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);        // Set the alpha values
        GUI.depth = drawDepth;      // Make the black Texture render on top (drawn last)
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }
    #endregion

    #region Start Fade effect
    protected virtual float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);     // Return to FadeSpeed variables so its easy to time Application.Wahtever you wanna do
    }
    #endregion

    #region When The Level Is Loaded
    protected virtual void OnLevelWasLoaded(int level)
    {
        alpha = 1;
        BeginFade(-1);
    }
    #endregion

    #region Entering The Trigger Zone
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(st_change_scene);
        }
    }
    #endregion
}
