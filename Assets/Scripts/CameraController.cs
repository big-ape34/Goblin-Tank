using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform target;        // The object to follow (e.g., your tank)

    [Header("Settings")]
    public float smoothTime = 0.2f; // Smaller = snappier, Larger = smoother
    public Vector3 offset = new Vector3(0, 0, -10); // Usually -10 for 2D camera

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    const float WorldHalfSize = 100f;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position with offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera toward the target
        Vector3 pos = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Clamp so camera doesn't show beyond world walls
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        pos.x = Mathf.Clamp(pos.x, -WorldHalfSize + halfWidth, WorldHalfSize - halfWidth);
        pos.y = Mathf.Clamp(pos.y, -WorldHalfSize + halfHeight, WorldHalfSize - halfHeight);

        transform.position = pos;
    }
}