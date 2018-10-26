using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GM : MonoBehaviour {

    //creating a singleton design pattern
    //GM can be accessed by every scene and data is global
    //GM is persistent

    #region Initiate
    public static GM sGM = null;// it's good to initialize variables to null

    //GM should be running from before any other script
    private void Awake()
    {   //check 9if this is the first time running gm
        if (sGM == null)
        {
            sGM = this;
            //DontDestroyOnLoad(sGM.gameObject);
        }
        else if (sGM != this)//otherwise, delete duplicate
        {
            Destroy(gameObject);
        }


    }
    #endregion
    // Use this for initialization
    [SerializeField]
    private GameObject PC;
    private Rigidbody2D mRBD;
    [SerializeField]
    private GameObject mCam;
    //[SerializeField]
    private Transform Player;
    [SerializeField]
    private GameObject Audio_Manager;
    [SerializeField]

    private Text text_stats;
    public bool bl_pause;
    public GameObject go_restartButton;
    public GameObject go_startmenu;
    public GameObject go_escape;
    public string st_startscene;
    [SerializeField]
    private GameObject BlackWall;

    private float fl_start_time;
    public static float fl_stop_time;
    public Vector2 V2_end_position;
    private Vector2 V2_start_position;
    private float fl_path_length;
    [SerializeField]
    private float fl_speed;
    private float tTimePow = 0;

    [SerializeField]
    private float fl_playerspeed;
    float adaptive_speed = -1;
    private string st_target;

    public static float Age;

    #region Camera Follow Variables
    // zeros out the velocity
    Vector3 velocity = Vector3.zero;

    // Time to folow Player
    public float smoothTime = .15f;

    //Enable and set the max Y value
    public bool yMaxEnable = false;
    public float yMaxValue = 0;

    //Enable and set the min Y value
    public bool yMinEnable = false;
    public float yMinValue = 0;

    //Enable and set the max X value
    public bool xMaxEnable = false;
    public float xMaxValue = 0;

    // Enable and set the min X value
    public bool xMinEnable = false;
    public float xMinValue = 0;

    #endregion

    // public DM_PC_Platform mMove;
    void Initialise()
    {
        //load the PC
        //SceneManager.LoadScene(st_startscene);
        CreatePC();
        //PCCamera();
        Audio_Manager_Spawn();

        
    }


    
        void Start ()
    {

        Initialise();
        V2_start_position =BlackWall.transform.position;

        fl_path_length = Vector2.Distance(V2_start_position, V2_end_position);
        fl_start_time = Time.time;
        mRBD = PC.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        //Age += Time.time * .25f;
        TimeWallManager();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || bl_pause)
        {
            bl_pause = false;
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        //add audio
        if (PC == null)
            PC = GameObject.FindGameObjectWithTag("Player");
      

    }

    //private void FixedUpdate()
    //{
    //    // Player Position
    //    Vector3 Playerpos = Player.position;

    //    // Vertical
    //    if (yMinEnable && yMaxEnable)
    //        Playerpos.y = Mathf.Clamp(Player.position.y, yMinValue, yMaxValue);

    //    else if (yMinEnable)
    //        Playerpos.y = Mathf.Clamp(Player.position.y, yMinValue, Player.position.y);

    //    else if (yMaxEnable)
    //        Playerpos.y = Mathf.Clamp(Player.position.y, Player.position.y, yMaxValue);

    //    // Horizontal
    //    if (xMinEnable && xMaxEnable)
    //        Playerpos.x = Mathf.Clamp(Player.position.x, xMinValue, xMaxValue);

    //    else if (xMinEnable)
    //        Playerpos.x = Mathf.Clamp(Player.position.x, xMinValue, Player.position.x);

    //    else if (xMaxEnable)
    //        Playerpos.x = Mathf.Clamp(Player.position.x, Player.position.x, xMaxValue);

    //    // Allign the camera and the Players z position
    //    Playerpos.z = transform.position.z;
    //    // Using Smooth damp we will gradually change the camera transform position to the Players position based on the cameras transform velocity and out smooth time
    //  mCam.transform.position = Vector3.SmoothDamp(transform.position, Playerpos, ref velocity, smoothTime);
    //}



    #region Menu
    //void OnGUI()
    //{
    //    if (Time.timeScale == 0)
    //    {
    //        //Display your gui.      
    //        go_startmenu.SetActive(true);
    //        go_restartButton.SetActive(true);
    //        go_escape.SetActive(true);

    //    }
    //    else
    //    {
    //        go_startmenu.SetActive(false);
    //        go_restartButton.SetActive(false);
    //        go_escape.SetActive(false);
    //        bl_pause = false;
    //    }

    //}
    //public void StartMenu()
    //{
    //    SceneManager.LoadScene(st_startscene);
    //    Time.timeScale = 1;
    //    go_startmenu.SetActive(false);
    //}
    //public void Escape()
    //{

    //    Application.Quit();

    //}
    //public void Pause()
    //{
    //    bl_pause = true;
    //}
    //public void RestartLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);//reload the scene.
    //    Time.timeScale = 1;
    //    go_restartButton.SetActive(false);

    //    //put achievments to 0
    //}
    #endregion 

    private void TimeWallManager()//this will follow the player through the game 
    {
        //mMove.MovePC();
       
       //fl_playerspeed = (mRBD.velocity.x)*0.001f;//get pc speed
                                                 //// float _fl_dist_travelled = (Tmcaime.time - fl_start_time) * Mathf.Pow(fl_speed, tTimePow);

        // //float tHeight = Camera.main.orthographicSize;//gathering height from the camera
        //// float tWidth = Camera.main.aspect * tHeight;// using height to deduce width from height)
        // float tConst;
        // tConst = Time.deltaTime;
        // if (fl_playerspeed < 0.001f)
        // {

        //     fl_speed += fl_playerspeed + adaptive_speed + Time.deltaTime;

        //     adaptive_speed = Mathf.Abs(fl_playerspeed * Mathf.Exp(Time.deltaTime) * 0.000001f);
        //     Time.timeScale = 0.5f;
        // }
        // else
        // {
        //     Time.timeScale =1;
                           
        //    fl_speed = (Mathf.Exp(Time.time*0.07f) * 0.01f);

        ////fl_speed += fl_playerspeed + adaptive_speed + Time.deltaTime;



        //BlackWall.transform.Translate(Vector3.right * fl_speed, Space.World);    


    }
    
    private void CreatePC()
    {
        Instantiate(sGM.PC, Vector3.zero, Quaternion.identity);
      
        //Player = PC.GetComponent<Transform>();
    }
    private void PCCamera()
    {
       // Instantiate(sGM.mCam, new Vector3 (0,0,-10), Quaternion.identity);
    }
    private void Audio_Manager_Spawn()
    {
        Instantiate(sGM.Audio_Manager, new Vector3(0, 0, 0), Quaternion.identity);
    }

}
