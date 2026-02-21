using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadScript : MonoBehaviour
{
    public GameObject expended;
    public GameObject unloaded;
    public GameObject loaded;

    public GameObject chamber;

    private bool isDragging = false;
    private float fixedX;
    private float yOffset;

    void Update()
    {
        // 1. Detect Click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = GetMouseWorldPos();
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.transform == transform)
            {
                isDragging = true;
                fixedX = transform.position.x; // Store original X
                yOffset = transform.position.y - mousePos.y; // Prevent snapping to center
            }
        }

        // 2. Drag Logic
        if (isDragging)
        {
            float newY = GetMouseWorldPos().y + yOffset;
            transform.position = new Vector3(fixedX, newY, transform.position.z);
        }

        // 3. Release
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        // Uses the object's current distance from camera for accurate conversion
        float zDepth = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDepth));
    }

}
