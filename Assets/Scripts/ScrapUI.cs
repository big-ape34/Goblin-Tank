using UnityEngine;
using TMPro;

public class ScrapUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scrapText;

    void Start()
    {
        UpdateText(0);
    }

    void OnEnable()
    {
        ScrapInventory.OnScrapChanged += UpdateText;
        GoblinMode.OnGoblinModeStarted += ShowGoblinMode;
        GoblinMode.OnGoblinModeEnded += ShowGoblinModeEnded;
    }

    void OnDisable()
    {
        ScrapInventory.OnScrapChanged -= UpdateText;
        GoblinMode.OnGoblinModeStarted -= ShowGoblinMode;
        GoblinMode.OnGoblinModeEnded -= ShowGoblinModeEnded;
    }

    void UpdateText(int scrapCount)
    {
        if (scrapText != null && !goblinModeActive)
            scrapText.text = "Scrap: " + scrapCount + "/16";
    }

    void ShowGoblinMode()
    {
        goblinModeActive = true;
        if (scrapText != null)
            scrapText.text = "Goblin Mode active!";
    }

    void ShowGoblinModeEnded()
    {
        goblinModeActive = false;
        UpdateText(0);
    }

    private bool goblinModeActive;
}
