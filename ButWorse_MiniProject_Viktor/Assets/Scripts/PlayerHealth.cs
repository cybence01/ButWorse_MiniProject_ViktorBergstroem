using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
[Header("Scene Management")]
[SerializeField] string mainMenuSceneName = "MainMenu";
    
[Header("Health Settings")]
[SerializeField] float maxHealth = 100f;
private float currentHealth;

[Header("UI References")]
[SerializeField] TextMeshProUGUI healthText;

[Header("Regeneration")]
[SerializeField] bool enableRegen = true;
[SerializeField] float regenDelay = 4f; //Delay before healing starts
[SerializeField] float regenSpeed = 10f; //Speed at which the player heals
private float timeSinceLastHit;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        if(enableRegen && currentHealth < maxHealth && currentHealth > 0)
        {
            timeSinceLastHit += Time.deltaTime;

            if (timeSinceLastHit > regenDelay)
            {
                currentHealth += regenSpeed * Time.deltaTime;

                currentHealth = Mathf.Min(currentHealth, maxHealth);
                UpdateHealthUI();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        timeSinceLastHit = 0f;

        Debug.Log("Player took damage");
        UpdateHealthUI();

        if (currentHealth <=0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text ="Health:" + Mathf.CeilToInt(currentHealth).ToString();
        }
    }

    private void Die()
    {
        Debug.Log("PLAYER DIED!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}