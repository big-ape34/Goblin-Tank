using UnityEngine;
using UnityEngine.InputSystem;

public class TorsoAnimController : MonoBehaviour
{
    [Header("Sprites for each direction")]
    [SerializeField] SpriteRenderer up;
    [SerializeField] SpriteRenderer down;
    [SerializeField] SpriteRenderer left;
    [SerializeField] SpriteRenderer right;
    [SerializeField] SpriteRenderer upLeft;
    [SerializeField] SpriteRenderer upRight;
    [SerializeField] SpriteRenderer downLeft;
    [SerializeField] SpriteRenderer downRight;

    private SpriteRenderer[] allDirections;
    private SpriteRenderer lastDirection;

    void Awake()
    {
        allDirections = new SpriteRenderer[]
        {
            up, down, left, right,
            upLeft, upRight, downLeft, downRight
        };
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Get mouse position in world space using Input System
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, Camera.main.nearClipPlane));

        Vector2 aimDir = mouseWorld - transform.position;

        SetDirection(aimDir);
    }

    void SetDirection(Vector2 aimDir)
    {
        if (aimDir.magnitude < 0.01f)
        {
            SetAllInvisible();
            if (lastDirection != null) lastDirection.enabled = true;
            return;
        }

        aimDir.Normalize();
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        SetAllInvisible();

        SpriteRenderer current = null;

        if (angle >= -22.5f && angle < 22.5f) current = right;
        else if (angle >= 22.5f && angle < 67.5f) current = upRight;
        else if (angle >= 67.5f && angle < 112.5f) current = up;
        else if (angle >= 112.5f && angle < 157.5f) current = upLeft;
        else if (angle >= 157.5f || angle < -157.5f) current = left;
        else if (angle >= -157.5f && angle < -112.5f) current = downLeft;
        else if (angle >= -112.5f && angle < -67.5f) current = down;
        else if (angle >= -67.5f && angle < -22.5f) current = downRight;

        if (current != null)
        {
            current.enabled = true;
            lastDirection = current;
        }
    }

    void SetAllInvisible()
    {
        foreach (var sr in allDirections)
        {
            if (sr != null) sr.enabled = false;
        }
    }
}
