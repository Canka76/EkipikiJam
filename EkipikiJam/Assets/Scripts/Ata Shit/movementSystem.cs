using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementSystem : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerWalkSpeed = 2.0f;
    public float playerRunSpeed = 4.0f;
    public float playerCrouchSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float speed = 0f;
    

    public Transform cameraTransform; 

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2.0f; 
    private float cameraPitch = 0f; 

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        // Fareyi kilitleme
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = gravityValue * Time.deltaTime;
        }

   
        HandleMouseLook();

  
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized; // Kameranın yatay düzlemi
        Vector3 move = cameraForward * moveInput.z + cameraTransform.right * moveInput.x;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = playerRunSpeed;
            controller.height = 2.0f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = playerCrouchSpeed;
            controller.height = 1.0f;
        }
        else
        {
            speed = playerWalkSpeed;
            controller.height = 2.0f;
        }


        controller.Move(move * Time.deltaTime * speed);


        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f); 
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
    }
}
