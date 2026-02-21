using UnityEngine;

public class ScrapInventory : MonoBehaviour
{
    public static event System.Action OnGoblinModeThresholdReached;
    public static event System.Action<int> OnScrapChanged;

    [SerializeField] private int threshold = 250;

    public int CurrentScrap { get; private set; }

    void OnEnable()
    {
        ScrapPickup.OnScrapCollected += AddScrap;
    }

    void OnDisable()
    {
        ScrapPickup.OnScrapCollected -= AddScrap;
    }

    public void AddScrap(int amount)
    {
        CurrentScrap += amount;
        OnScrapChanged?.Invoke(CurrentScrap);

        if (CurrentScrap >= threshold)
            OnGoblinModeThresholdReached?.Invoke();
    }

    public void RemoveScrap(int amount)
    {
        CurrentScrap = Mathf.Max(0, CurrentScrap - amount);
        OnScrapChanged?.Invoke(CurrentScrap);
    }

    public void SetScrap(int value)
    {
        CurrentScrap = Mathf.Max(0, value);
        OnScrapChanged?.Invoke(CurrentScrap);
    }
}
