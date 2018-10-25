using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_FPSController : MonoBehaviour {

    /// Moving information

    // Speed of player
    public float speed = 6.0F;
    // Mouse Movement movement speed
    public float sensitivity = 2f;
    // Independant game component 
    CharacterController player;
    // Move Front/Back 
    float moveFB;
    // Move Left/Right
    float moveLR;
    // Rotating mouse on X axis
    float rotX;
    // Rotating mouse on Y axis 
    float rotY;
    // Finding the camera
    public GameObject eyes;
    // Move speed (Mouse)
    public float MouseTurnRate = 90F;
    // Jump Speed/Height
    public float jumpSpeed = 8.0F;
    // jumping limits
    public float gravity = 20.0F;
    // private axis 
    private Vector3 moveDirection = Vector3.zero;
    // Double MoveSpeed
    private float Doublespeed;

    void Start()
    {
        // Calling character controller component from inspector
        player = GetComponent<CharacterController>();
        // Double speed while holding shift
        Doublespeed = speed;

    }



    void Update()
    {
        MovePC();
    }
    void MovePC()
    {
        transform.Rotate(0, MouseTurnRate * Time.deltaTime * Input.GetAxis("Mouse X"), 0);
        
        // If the run key is down double the speed
        if (Input.GetKey(KeyCode.LeftShift))
            speed = Doublespeed * 2;
        else
            speed = Doublespeed;

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
         
               

            // Moving Up/Down
            moveFB = Input.GetAxis("Vertical") * speed;
            // Moving Side to side
            moveLR = Input.GetAxis("Horizontal") * speed;
            

            // Making Mouse rotate on the X axis
            rotX = Input.GetAxis("Mouse X") * sensitivity;
            // Making Mouse rotate on the Y axis
            rotY -= Input.GetAxis("Mouse Y") * sensitivity;
           rotY = Mathf.Clamp(rotY, -60f, 60f);

            // where the player will move
            Vector3 movement = new Vector3(moveLR, 0, moveFB);

            // Moving the player eyes (Camera)
            transform.Rotate(0, rotX, 0);
            eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

            // Showing player rotation
            movement = transform.rotation * movement;

            // Telling the player to move
            player.Move(movement * Time.deltaTime);

            // Axis where the player will Jump
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
}