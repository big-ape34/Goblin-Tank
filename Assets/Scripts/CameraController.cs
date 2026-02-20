using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform target;        // The object to follow (e.g., your tank)

    [Header("Settings")]
    public float smoothTime = 0.2f; // Smaller = snappier, Larger = smoother
    public Vector3 offset = new Vector3(0, 0, -10); // Usually -10 for 2D camera

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position with offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera toward the target
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}