using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{

    public bool stalled = false;
    [HideInInspector] public bool stallOverride = false;
    public float moveSpeed = 5f;
    [HideInInspector] public float speedMultiplier = 1f;
    public float rotationSpeed = 150f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null) return;

        Vector2 moveDir = Vector2.zero;

        if (!stalled || stallOverride)
        {
            // WASD input
            if (Keyboard.current.wKey.isPressed) moveDir.y += 1f;
            if (Keyboard.current.sKey.isPressed) moveDir.y -= 1f;
            if (Keyboard.current.dKey.isPressed) moveDir.x += 1f;
            if (Keyboard.current.aKey.isPressed) moveDir.x -= 1f;

            moveDir.Normalize(); // prevents faster diagonal movement
            moveSpeed = Keyboard.current.shiftKey.isPressed ? 10f : 5f;
        }

        rb.linearVelocity = moveDir * moveSpeed * speedMultiplier;
    }
}
