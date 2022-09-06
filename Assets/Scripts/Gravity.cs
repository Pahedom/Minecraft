using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    public float acceleration = -32f;
    public float terminalVelocity = -78.4f;
    public bool disableGravity = false;

    [Header("Ground Detection")]
    public Transform groundChecker;
    public LayerMask layerMask;
    public Vector3 checkBoxExtents = new Vector3(0.299f, 0.1f, 0.299f);
    public float groundedVelocity = 0f;

    internal float velocity = 0f;
    internal bool isGrounded = false;

    private Rigidbody _myRigidbody;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();

        ApplyGravity();
    }

    private void CheckGround()
    {
        if (disableGravity || groundChecker == null)
        {
            isGrounded = false;
            return;
        }

        bool wasGrounded = isGrounded;

        isGrounded = velocity <= 0f && Physics.CheckBox(groundChecker.position, checkBoxExtents, Quaternion.identity, layerMask);

        if (wasGrounded && !isGrounded && velocity < 0f)
        {
            velocity = -0.1f;
        }
    }

    private void ApplyGravity()
    {
        if (disableGravity)
        {
            velocity = 0f;
            return;
        }

        if (isGrounded)
        {
            velocity = groundedVelocity;
        }
        else
        {
            velocity += acceleration * Time.deltaTime;

            if (velocity < terminalVelocity)
            {
                velocity = terminalVelocity;
            }
        }

        _myRigidbody.velocity = new Vector3(_myRigidbody.velocity.x, velocity, _myRigidbody.velocity.z);
    }
}