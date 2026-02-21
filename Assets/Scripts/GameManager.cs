using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOverUI;

    private TankMovement tankMovement;
    private CombatController combatController;
    private HeatGague heatGague;
    private EnemySpawner enemySpawner;
    private bool isGameOver;

    void Start()
    {
        GameObject tank = GameObject.FindGameObjectWithTag("Tank");
        if (tank != null)
        {
            tankMovement = tank.GetComponent<TankMovement>();
            combatController = tank.GetComponentInChildren<CombatController>();
            heatGague = tank.GetComponent<HeatGague>();
        }

        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    void Update()
    {
        if (isGameOver)
            return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void HandlePlayerDeath()
    {
        isGameOver = true;

        if (tankMovement != null)
            tankMovement.enabled = false;

        if (combatController != null)
            combatController.enabled = false;

        if (heatGague != null)
            heatGague.enabled = false;

        int kills = enemySpawner != null ? enemySpawner.TotalKills : 0;

        if (gameOverUI != null)
            gameOverUI.Show(kills);
    }
}
