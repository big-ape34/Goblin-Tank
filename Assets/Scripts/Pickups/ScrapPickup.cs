using UnityEngine;

public class ScrapPickup : MonoBehaviour
{
    public static event System.Action<int> OnScrapCollected;

    private int scrapValue = 1;
    private Vector3 driftTarget;
    private float driftDuration = 0.3f;
    private float driftTimer;
    private Vector3 startPosition;

    public void Initialize(int value)
    {
        scrapValue = value;
    }

    void Start()
    {
        startPosition = transform.position;
        driftTarget = startPosition + (Vector3)(Random.insideUnitCircle * 0.5f);
        driftTimer = 0f;
    }

    void Update()
    {
        if (driftTimer < driftDuration)
        {
            driftTimer += Time.deltaTime;
            float t = Mathf.Clamp01(driftTimer / driftDuration);
            transform.position = Vector3.Lerp(startPosition, driftTarget, t);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tank"))
        {
            OnScrapCollected?.Invoke(scrapValue);
            Destroy(gameObject);
        }
    }
}
