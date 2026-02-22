using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

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

    void Awake()
    {
        player = GameObject.Find("Tank").transform;

        allDirections = new GameObject[]
        {
            walkUp, walkDown, walkLeft, walkRight,
            walkUpLeft, walkUpRight, walkDownLeft, walkDownRight
        };
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        UpdateAnimation(direction);
    }

    void UpdateAnimation(Vector2 dir)
    {
        GameObject directionToActivate = GetDirectionObject(dir);

        if (directionToActivate == lastDirection)
            return;

        foreach (GameObject obj in allDirections)
            obj.SetActive(false);

        directionToActivate.SetActive(true);
        lastDirection = directionToActivate;
    }

    GameObject GetDirectionObject(Vector2 dir)
    {
        dir.Normalize();

        if (dir.y > 0.5f)
        {
            if (dir.x > 0.5f) return walkUpRight;
            if (dir.x < -0.5f) return walkUpLeft;
            return walkUp;
        }
        else if (dir.y < -0.5f)
        {
            if (dir.x > 0.5f) return walkDownRight;
            if (dir.x < -0.5f) return walkDownLeft;
            return walkDown;
        }
        else
        {
            if (dir.x > 0) return walkRight;
            return walkLeft;
        }
    }
}