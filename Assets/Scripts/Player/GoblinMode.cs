using System.Collections;
using UnityEngine;

public class GoblinMode : MonoBehaviour
{
    [SerializeField] private GameObject goblinModeEffect;

    public static event System.Action OnGoblinModeStarted;
    public static event System.Action OnGoblinModeEnded;

    [SerializeField] private float duration = 8f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float scrapDrainInterval = 0.5f;
    [SerializeField] private int scrapDrainAmount = 16;

    public bool IsActive { get; private set; }

    private TankMovement tankMovement;
    private PlayerHealth playerHealth;
    private InvincibilityFlash flash;
    private ScrapInventory scrapInventory;
    private HeatGague heatGague;
    private Coroutine drainCoroutine;
    private float savedHeat;

    void Awake()
    {
        tankMovement = GetComponent<TankMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        flash = GetComponent<InvincibilityFlash>();
        scrapInventory = GetComponent<ScrapInventory>();
        heatGague = GetComponent<HeatGague>();
    }

    void OnEnable()
    {
        ScrapInventory.OnGoblinModeThresholdReached += Activate;
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        ScrapInventory.OnGoblinModeThresholdReached -= Activate;
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        if (tankMovement != null)
        {
            tankMovement.speedMultiplier = speedMultiplier;
            tankMovement.stallOverride = true;
        }

        if (heatGague != null)
        {
            savedHeat = heatGague.heat;
            heatGague.heat = 0f;
            heatGague.suspended = true;
        }

        if (playerHealth != null)
            playerHealth.SetExternalInvincible(true);

        if (flash != null)
            flash.StartFlash(duration);

        drainCoroutine = StartCoroutine(GoblinModeRoutine());

        OnGoblinModeStarted?.Invoke();
    }

    void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;

        if (tankMovement != null)
        {
            tankMovement.speedMultiplier = 1f;
            tankMovement.stallOverride = false;
        }

        if (heatGague != null)
        {
            heatGague.heat = savedHeat;
            heatGague.suspended = false;
        }

        if (playerHealth != null)
            playerHealth.SetExternalInvincible(false);

        if (flash != null)
            flash.StopFlash();

        if (scrapInventory != null)
            scrapInventory.SetScrap(0);

        OnGoblinModeEnded?.Invoke();
    }

    IEnumerator GoblinModeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(scrapDrainInterval);
            elapsed += scrapDrainInterval;

            if (scrapInventory != null)
                scrapInventory.RemoveScrap(scrapDrainAmount);

            //here??
            goblinModeEffect.SetActive(true);
        }

        drainCoroutine = null;
        Deactivate();

        goblinModeEffect.SetActive(false);
    }

    void HandlePlayerDeath()
    {
        if (!IsActive)
            return;

        if (drainCoroutine != null)
        {
            StopCoroutine(drainCoroutine);
            drainCoroutine = null;
        }

        Deactivate();
        //here??
        goblinModeEffect.SetActive(false);

    }
}
