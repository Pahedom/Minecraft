using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    float mouseX;
    float mouseY;

    Vector3 headRotation = new Vector3(0f, 0f, 0f);
    Vector3 bodyRotation = new Vector3(0f, 0f, 0f);

    string walkingDirection = "none";

    Camera myCamera;

    public float mouseSense;
    public float FOV;

    public Transform steveController;
    public Transform steveBody;
    public Transform steveHead;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        myCamera = GetComponent<Camera>();

        myCamera.fieldOfView = FOV;
    }

    void Update()
    {
        MoveHeadAndBody();
    }

    void MoveHeadAndBody()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        headRotation.x -= mouseY * mouseSense * Time.deltaTime;
        headRotation.x = Mathf.Clamp(headRotation.x, -90f, 90f);

        headRotation.y += mouseX * mouseSense * Time.deltaTime;
        headRotation.y = Mathf.Clamp(headRotation.y, -45f, 45f);
        
        if (walkingDirection == "none")
        {
            // Move head and body
            if ((headRotation.y == 45f && mouseX > 0f) || (headRotation.y == -45f && mouseX < 0f))
            {
                bodyRotation.y += mouseX * mouseSense * Time.deltaTime;

                steveController.rotation = Quaternion.Euler(0f, bodyRotation.y, 0f);

                steveHead.localRotation = Quaternion.Euler(headRotation.x, steveHead.localRotation.eulerAngles.y, 0f);
            }
            // Move just head
            else
            {
                steveHead.localRotation = Quaternion.Euler(headRotation.x, headRotation.y, 0f);
            }
        }
        else
        {
            steveHead.localRotation = Quaternion.Euler(headRotation.x, headRotation.y, 0f);

            steveHead.transform.parent = null;

            steveController.rotation = Quaternion.Euler(0f, steveHead.rotation.eulerAngles.y, 0f);

            steveHead.transform.parent = steveController;

            bodyRotation.y = steveController.rotation.eulerAngles.y;

            headRotation.y = 0f;
        }

        if (walkingDirection != "none")
        {
            //if (walkingDirection != "right" && walkingDirection != "left")
            //{
                steveBody.localRotation = Quaternion.Euler(0f, 0f, 0f);
            //}
            //else if (walkingDirection == "right")
            //{
            //    steveBody.localRotation = Quaternion.Euler(0f, 45f, 0f);
            //}
            //else if (walkingDirection == "left")
            //{
            //    steveBody.localRotation = Quaternion.Euler(0f, -45f, 0f);
            //}
        }

        walkingDirection = "none";
    }

    public void ChangeWalkingDirection(string newDirection)
    {
        walkingDirection = newDirection;
    }
}
