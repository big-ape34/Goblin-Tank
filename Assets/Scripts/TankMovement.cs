using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{

    [SerializeField] private GameObject _tankBody;
    public float moveSpeed = 5f;
    public float rotationSpeed = 150f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = _tankBody.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null) return;

        float moveInput = 0f;
        float rotationInput = 0f;

        // W / S = forward & backward
        if (Keyboard.current.wKey.isPressed) moveInput = 1f;
        if (Keyboard.current.sKey.isPressed) moveInput = -1f;

        // A / D = rotate
        if (Keyboard.current.aKey.isPressed) rotationInput = 1f;
        if (Keyboard.current.dKey.isPressed) rotationInput = -1f;

        // Move
        Vector2 movement = transform.up * moveInput * moveSpeed;
        rb.linearVelocity = movement;

        // Rotate
        float rotation = -rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }
}
