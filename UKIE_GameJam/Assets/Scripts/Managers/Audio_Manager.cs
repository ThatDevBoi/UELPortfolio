using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Manager : Switch_Scene
{
    #region Variables
    public static Audio_Manager sGM = null;      // GameObject singletone reference Starts as Null [Null = Nothing]
    public AudioSource My_audiosource;      // Audio Source reference
    public AudioClip[] BackGround_Music;        // Array of BackGround Music
    public AudioMixer audio_Mixer;      // Reference to the master audio mixer
    #endregion

    #region Awake Function
    private void Awake()
    {
        My_audiosource = GetComponent<AudioSource>();       // Finding reference
    }
    #endregion

    #region Start Function
    // Use this for initialization
    void Start()
    {
        // Functions
        Baby_BM();
    }
    #endregion

    public void VolumeSetting(float volume)
    {
        audio_Mixer.SetFloat("Volume", volume);     // Setting the AudioMixer value to whatever the player decides
    }

    #region Override Base Function OnLevelWasLoaded
    protected override void OnLevelWasLoaded(int level)
    {
        base.OnLevelWasLoaded(level);       // Calling the base script (Switch Scenes) Function
    }
    #endregion

    #region Begin The Fade
    protected override float BeginFade(int direction)
    {
        return base.BeginFade(direction);       // Call function from base source
    }
    #endregion

    #region Baby BackGround Music
    public void Baby_BM()
    {
        My_audiosource.clip = BackGround_Music[0];
        My_audiosource.Play();
    }
    #endregion

    #region Adult BackGround Music
    public void Adult_BM()
    {

            My_audiosource.Stop();      // Stop audio source
            My_audiosource.clip = BackGround_Music[1];      // Change Audio clip from array
            My_audiosource.Play();      // Play the new clip
    }
    #endregion

    #region OnTriggerEnter
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Baby_BM")       // If gameObject hits a trigger with the tag Baby_BM
        {
            alpha = 1;      // Alpha sets to 1
            BeginFade(-1);      // Call Function
            Baby_BM();      // Call Audio Source Start Function
        }

        if(other.gameObject.tag == "Adult_BM")      // If gameObject hits a trigger with the tag Adult_BM
        {
            alpha = 1;      // Set Alpha value from base script to 1
            BeginFade(-1);      // Call Base Function
            Adult_BM();     // Call Function Plays new music for Adult Player
        }
    }
    #endregion
}
