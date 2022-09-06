using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class StairStepping : Debuggable
{
    public float stepOffset = 0.5f;
    public LayerMask layerMask;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private const float _errorMargin = 0.0001f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StepUp(collision.collider);
    }
    private void OnCollisionStay(Collision collision)
    {
        StepUp(collision.collider);
    }

    private void StepUp(Collider collider)
    {
        if (!collider.gameObject.IsInLayerMask(layerMask))
        {
            DebugLog("Couldn't step up: wrong layer");
            return;
        }

        if (!_rigidbody.velocity.y.IsBetween(-_errorMargin, _errorMargin))
        {
            DebugLog("Couldn't step up: moving on Y axis");
            return;
        }
        if (_rigidbody.velocity.x == 0f && _rigidbody.velocity.z == 0f)
        {
            DebugLog("Couldn't step up: not moving on X and Z axes");
            return;
        }

        float otherMaxBound = collider.bounds.max.y;
        float myMinBound = _collider.bounds.min.y;

        if (otherMaxBound > myMinBound + stepOffset + _errorMargin)
        {
            DebugLog("Couldn't step up: object is too high");
            return;
        }
        if (otherMaxBound <= myMinBound + _errorMargin)
        {
            DebugLog("Couldn't step up: object is too low");
            return;
        }

        Vector3 translation = Vector3.zero;
        if (_rigidbody.velocity.x != 0f)
        {
            translation.x = 0.02f;
        }
        if (_rigidbody.velocity.z != 0f)
        {
            translation.z = 0.02f;
        }
        translation.y = otherMaxBound - myMinBound + _errorMargin;

        if (Physics.CheckBox(_collider.bounds.center + translation, _collider.bounds.extents, Quaternion.identity, layerMask))
        {
            DebugLog("Couldn't step up: obstructed");
            return;
        }

        _rigidbody.MovePosition(transform.position + translation);
        DebugLog("Stepped up");
    }
}