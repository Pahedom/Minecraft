using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FPPCamera : MonoBehaviour
{
    public Transform body;
    public Transform head;

    public float sense = 350f;
    public Range constraintsX = new Range(-90f, 90f);

    private float _rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MoveCamera(Vector2 mouseAxis)
    {
        mouseAxis *= sense * Time.deltaTime;

        body.Rotate(Vector3.up * mouseAxis.x);

        _rotationX -= mouseAxis.y;
        _rotationX.Clamp(constraintsX);

        head.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }
}