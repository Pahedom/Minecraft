using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Gravity))]
public class CharacterMovement : MonoBehaviour
{
    public Transform body;

    public float walkingSpeed = 4.317f;
    public float sneakingSpeed = 1.295f;
    public float sprintingSpeed = 5.612f;
    public float jumpHeight = 1.25f;

    private Rigidbody _rigidbody;
    private Gravity _gravity;

    private float _currentSpeed;
    private bool _sneaking = false;
    private bool _sprinting = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = GetComponent<Gravity>();

        _currentSpeed = walkingSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            StopSprinting();
        }

        Vector3 localDirection = body.right * direction.x + body.forward * direction.z;

        if (localDirection.magnitude > 1f)
        {
            localDirection.Normalize();
        }

        Vector3 newVelocity = localDirection * _currentSpeed;

        newVelocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = newVelocity;
    }

    public void Jump()
    {
        if (_gravity.isGrounded)
        {
            _gravity.velocity = Mathf.Sqrt(jumpHeight * -2f * _gravity.acceleration);
        }
    }

    public void StartSneaking()
    {
        if (_sneaking)
        {
            return;
        }

        _sneaking = true;
        _currentSpeed = sneakingSpeed;
    }
    public void StopSneaking()
    {
        if (!_sneaking)
        {
            return;
        }

        _sneaking = false;

        if (_sprinting)
        {
            _currentSpeed = sprintingSpeed;
        }
        else
        {
            _currentSpeed = walkingSpeed;
        }
    }

    public void StartSprinting()
    {
        if (_sprinting || _sneaking)
        {
            return;
        }

        _sprinting = true;
        _currentSpeed = sprintingSpeed;
    }
    public void StopSprinting()
    {
        if (!_sprinting)
        {
            return;
        }

        _sprinting = false;
        _currentSpeed = walkingSpeed;
    }
}