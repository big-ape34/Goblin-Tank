using UnityEngine;
using UnityEngine.InputSystem;

public class BodyAnimController : MonoBehaviour
{
    [Header("Animation GameObjects")]
    [SerializeField] GameObject walkUp;
    [SerializeField] GameObject walkDown;
    [SerializeField] GameObject walkLeft;
    [SerializeField] GameObject walkRight;
    [SerializeField] GameObject walkUpLeft;
    [SerializeField] GameObject walkUpRight;
    [SerializeField] GameObject walkDownLeft;
    [SerializeField] GameObject walkDownRight;

    private GameObject[] allDirections;
    private GameObject lastDirection;

    void Start()
    {
        lastDirection = walkDown;   // choose your default
        SetAllInvisible();
        SetVisible(lastDirection);

        var anim = lastDirection.GetComponent<Animator>();
        if (anim != null)
            anim.enabled = false; // idle/frozen at start
    }
    void Awake()
    {
        allDirections = new GameObject[]
        {
            walkUp, walkDown, walkLeft, walkRight,
            walkUpLeft, walkUpRight, walkDownLeft, walkDownRight
        };
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        Vector2 moveDir = Vector2.zero;

        // WASD input
        if (Keyboard.current.wKey.isPressed) moveDir.y += 1f;
        if (Keyboard.current.sKey.isPressed) moveDir.y -= 1f;
        if (Keyboard.current.dKey.isPressed) moveDir.x += 1f;
        if (Keyboard.current.aKey.isPressed) moveDir.x -= 1f;

        if (moveDir == Vector2.zero)
        {
            FreezeLastDirection();
            return;
        }

        moveDir.Normalize();
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        // Make all invisible first
        SetAllInvisible();

        // Determine which direction to show
        GameObject current = null;
        if (angle >= -22.5f && angle < 22.5f) current = walkRight;
        else if (angle >= 22.5f && angle < 67.5f) current = walkUpRight;
        else if (angle >= 67.5f && angle < 112.5f) current = walkUp;
        else if (angle >= 112.5f && angle < 157.5f) current = walkUpLeft;
        else if (angle >= 157.5f || angle < -157.5f) current = walkLeft;
        else if (angle >= -157.5f && angle < -112.5f) current = walkDownLeft;
        else if (angle >= -112.5f && angle < -67.5f) current = walkDown;
        else if (angle >= -67.5f && angle < -22.5f) current = walkDownRight;

        if (current != null)
        {
            SetVisible(current);
            // Make sure its Animator is playing
            var anim = current.GetComponent<Animator>();
            if (anim != null) anim.enabled = true;

            lastDirection = current;
        }
    }

    void SetAllInvisible()
    {
        foreach (var go in allDirections)
        {
            if (go == null) continue;
            var sr = go.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;

            // Keep Animator running, but you could also disable here if needed
            var anim = go.GetComponent<Animator>();
            if (anim != null) anim.enabled = true;
        }
    }

    void SetVisible(GameObject go)
    {
        if (go == null) return;
        var sr = go.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = true;
    }

    void FreezeLastDirection()
    {
        // Make all invisible
        foreach (var go in allDirections)
        {
            if (go == null) continue;
            var sr = go.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;

            // Pause Animator
            var anim = go.GetComponent<Animator>();
            if (anim != null) anim.enabled = false;
        }

        // Show and freeze last direction
        if (lastDirection != null)
        {
            var sr = lastDirection.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;
        }
    }
}