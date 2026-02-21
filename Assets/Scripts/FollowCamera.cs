using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Tooltip("Viewport position (0-1) where this object is anchored. (1,1) = top-right corner.")]
    public Vector2 viewportOffset = new Vector2(0.85f, 0.85f);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 worldPos = mainCamera.ViewportToWorldPoint(new Vector3(viewportOffset.x, viewportOffset.y, 0f));
        worldPos.z = transform.position.z;
        transform.position = worldPos;
    }
}
