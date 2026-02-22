using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private Vector3 originalBarScale;

    public static event System.Action OnPlayerDeath;
    public static event System.Action<float, float> OnHealthChanged;
    public static event System.Action OnDamageTaken;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float invincibilityDuration = 1f;

    public float CurrentHealth { get; private set; }
    public bool IsInvincible => invincibilityTimer > 0f || externalInvincible;
    public bool IsDead { get; private set; }

    private float invincibilityTimer;
    private bool externalInvincible;
    private InvincibilityFlash flash;

    void Awake()
    {
        CurrentHealth = maxHealth;
        flash = GetComponent<InvincibilityFlash>();

        if (healthBar != null)
            originalBarScale = healthBar.transform.localScale;
    }

    void Update()
    {
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                if (!externalInvincible && flash != null)
                    flash.StopFlash();
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (IsDead || IsInvincible)
            return;

        CurrentHealth -= amount;
        if (CurrentHealth < 0f)
            CurrentHealth = 0f;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        UpdateHealthBar();
        OnDamageTaken?.Invoke();

        if (CurrentHealth <= 0f)
        {
            IsDead = true;
            OnPlayerDeath?.Invoke();
            return;
        }

        // Start invincibility frames
        invincibilityTimer = invincibilityDuration;
        if (flash != null)
            flash.StartFlash(invincibilityDuration);
    }

    public void SetExternalInvincible(bool value)
    {
        externalInvincible = value;
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null) return;

        float percent = CurrentHealth / maxHealth;

        Vector3 newScale = originalBarScale;
        newScale.x = originalBarScale.x * percent;

        healthBar.transform.localScale = newScale;
    }
}
