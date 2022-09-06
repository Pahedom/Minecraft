using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public FPPCamera fPPCamera;
    public CharacterMovement movement;
    public BlockInteractions blockInteractions;
    public QuickSlots quickSlots;

    private void Update()
    {
        MoveCamera();

        Move();

        Jump();

        Sneak();

        Sprint();

        DestroyBlock();

        CreateBlock();

        SelectQuickSlot();
    }

    private void MoveCamera()
    {
        fPPCamera.MoveCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
    }

    private void Move()
    {
        movement.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            movement.Jump();
        }
    }

    private void Sneak()
    {
        if (Input.GetButtonDown("Sneak"))
        {
            movement.StartSneaking();
        }
        else if (Input.GetButtonUp("Sneak"))
        {
            movement.StopSneaking();
        }
    }

    private void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            movement.StartSprinting();
        }
    }

    private void DestroyBlock()
    {
        if (Input.GetButtonDown("Destroy"))
        {
            blockInteractions.Destroy();
        }
    }

    private void CreateBlock()
    {
        if (Input.GetButtonDown("Create"))
        {
            blockInteractions.Create();
        }
    }

    private void SelectQuickSlot()
    {
        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scrollWheel < 0)
        {
            quickSlots.SelectNext();
        }
        else if (scrollWheel > 0)
        {
            quickSlots.SelectPrevious();
        }
    }
}