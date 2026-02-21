using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event System.Action OnPlayerDeath;
    public static event System.Action<float, float> OnHealthChanged;
    public static event System.Action OnDamageTaken;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float invincibilityDuration = 1f;

    public float CurrentHealth { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool IsDead { get; private set; }

    private float invincibilityTimer;
    private InvincibilityFlash flash;

    void Awake()
    {
        CurrentHealth = maxHealth;
        flash = GetComponent<InvincibilityFlash>();
    }

    void Update()
    {
        if (IsInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                IsInvincible = false;
                if (flash != null)
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
        OnDamageTaken?.Invoke();

        if (CurrentHealth <= 0f)
        {
            IsDead = true;
            OnPlayerDeath?.Invoke();
            return;
        }

        // Start invincibility frames
        IsInvincible = true;
        invincibilityTimer = invincibilityDuration;
        if (flash != null)
            flash.StartFlash(invincibilityDuration);
    }
}
