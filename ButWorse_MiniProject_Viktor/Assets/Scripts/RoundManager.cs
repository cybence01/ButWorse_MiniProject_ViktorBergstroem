using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("Round Settings")]
    [SerializeField] private int currentRound = 0;
    [SerializeField] private float timeBetweenRounds = 5f;
    [SerializeField] private int baseEnemiesPerRound = 6;
    [SerializeField] private float enemyScalingFactor = 0.15f;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI roundText;
    private int enemiesAlive = 0;
    private int enemiesToSpawn = 0;
    private bool roundActive = false;

    public int CurrentRound => currentRound;
    public int EnemiesAlive => enemiesAlive;
    public int EnemiesToSpawn => enemiesToSpawn;
    public bool IsRoundActive => roundActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Delay first round to ensure all listeners are subscribed
        Invoke(nameof(StartNewRound), 0.5f);
    }

    public void StartNewRound()
    {
        currentRound++;
        roundActive = true;

        // Calculate enemies for this round (COD Zombies formula)
        enemiesToSpawn = CalculateEnemiesForRound(currentRound);
    }

    private int CalculateEnemiesForRound(int round)
    {
        // Similar to COD Zombies scaling
        // Round 1: 6 enemies
        // Each round adds more based on scaling factor
        float enemies = baseEnemiesPerRound * Mathf.Pow(1 + enemyScalingFactor, round - 1);
        return Mathf.RoundToInt(enemies);
    }

    public void EnemySpawned()
    {
        enemiesToSpawn--;
        enemiesAlive++;
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        // Check if round is complete
        if (enemiesAlive <= 0 && enemiesToSpawn <= 0 && roundActive)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        roundActive = false;

        Invoke(nameof(StartNewRound), timeBetweenRounds);
    }

    public void ResetRounds()
    {
        currentRound = 0;
        enemiesAlive = 0;
        enemiesToSpawn = 0;
        roundActive = false;
        CancelInvoke();
    }

    private void Update()
    {
        roundText.text = $"Current Round: " + currentRound;
    }
}