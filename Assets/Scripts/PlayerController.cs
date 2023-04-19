using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float maxX = 85;
    float minX = -85;
    public Transform PlayerTransform;
    public float sensitivity = 2f;
    float cameraPitch = 0f;
    float walkSpeed = 2f;
    [Range(0.0f, 0.5f)] public float moveSmoothTime = 0.3f;
    [Range(0.0f, 0.5f)] public float mouseSmoothTime = 0.3f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    CharacterController controller;
    GameManager manager;
    // Start is called before the first frame update
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
        float inputX = Input.GetAxis("Mouse X") ;
        float inputY = Input.GetAxis("Mouse Y") ;
        Vector2 targetMouseDelta = new Vector2(inputX, inputY);

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * sensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, minX, maxX);
        PlayerTransform.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    public void updateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed;

        controller.Move(velocity);
    }
}
