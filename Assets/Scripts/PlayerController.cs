using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float maxX = 85;
    float minX = -85;
    public Transform CameraTransform;
    public float sensitivity = 100f;
    public float walkSpeed = 12f;
    public float jumpHeight;

    float xRotation = 0f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    bool isGrounded;

    public float gravity = -9.81f;
    Vector3 velocity;

    CharacterController controller;
    GameManager manager;

    public HealthBar health;
    void Start()
    {
        manager = GameManager.instance;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null)
        {
            manager = GameManager.instance;
        }
        //If paused, disable turning and moving
        if (manager.getIsPaused()) { return; }
        //Else
        updateMouseLook();
        updateMovement();
    }

    public void updateMouseLook()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float inputX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);
        CameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * inputX);
    }

    public void updateMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void hurt(float dmg)
    {
        health.Damage(dmg);
    }
}
