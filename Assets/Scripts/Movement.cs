using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    float x;
    float z;

    Vector3 movementVector;

    Vector3 velocity;

    CharacterController myCharacterController;

    float speed;

    public bool sprinting;
    bool canSprint = true;
    bool sneaking;
    bool moving;
    
    public float walkingSpeed;
    public float sprintingSpeed;
    public float sneakingSpeed;
    public float jumpHeight;
    public float gravity;

    public float checkRadius;

    bool isGrounded;

    public Transform groundCheck;
    public LayerMask blockMask;

    public UnityEvent OnWalkingForward;
    public UnityEvent OnWalkingBackward;
    public UnityEvent OnWalkingRight;
    public UnityEvent OnWalkingLeft;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        x = 0;
        z = 0;

        moving = false;
        
        if (Input.GetKey(KeyCode.W))
        {
            OnWalkingForward.Invoke();

            z += 1;

            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnWalkingBackward.Invoke();

            z -= 1;

            moving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            OnWalkingLeft.Invoke();

            x -= 1;

            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnWalkingRight.Invoke();

            x += 1;

            moving = true;
        }

        if (!canSprint || !moving)
        {
            sprinting = false;
        }
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sneaking = true;
            
            speed = sneakingSpeed;
        }
        else
        {
            sneaking = false;

            if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint)
            {
                sprinting = true;
            }
            
            if (sprinting)
            {
                speed = sprintingSpeed;
            }
            else
            {
                speed = walkingSpeed;
            }
        }

        //if ()
        
        movementVector = transform.right * x + transform.forward * z;

        myCharacterController.Move(movementVector * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        myCharacterController.Move(velocity * Time.deltaTime);

        Debug.Log(isGrounded);
    }

    public void GroundCheck(bool newGroundCheck)
    {
        isGrounded = newGroundCheck;
    }

    public void CollisionCheck(bool newCollisionCheck)
    {
        canSprint = !newCollisionCheck;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
