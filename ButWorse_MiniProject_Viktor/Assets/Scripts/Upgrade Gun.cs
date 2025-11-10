using UnityEngine;
using TMPro;

public class UpgradeGun : MonoBehaviour
{
    private float upgradeCost = 5000f;
    private KeyCode upgradeKey = KeyCode.E;
    [Header("Upgrade Stats")]
    [SerializeField] private float damageIncrease = 15f; // How much damage increases per upgrade

    [Header("References")]
    [SerializeField] private GunController gunController;
    [SerializeField] private TextMeshProUGUI upgradePromptText;

    private int upgradeLevel = 0;
    private bool canAffordUpgrade = false;

    void Start()
    {
        if (gunController == null)
        {
            gunController = GetComponent<GunController>();
        }

        if (upgradePromptText != null)
        {
            upgradePromptText.gameObject.SetActive(false);
        }
    }
    
    private void CheckUpgradeAvailability()
    {
        if (RoundManager.Instance == null) return;
        
        canAffordUpgrade = RoundManager.Instance.currentPoints >= upgradeCost;
        
        // Show/hide upgrade prompt
        if (upgradePromptText != null)
        {
            if (canAffordUpgrade)
            {
                upgradePromptText.gameObject.SetActive(true);
                upgradePromptText.text = $"Press [{upgradeKey}] to Upgrade Gun (${upgradeCost})";
            }
            else
            {
                upgradePromptText.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        CheckUpgradeAvailability();
        
        // Handle upgrade input
        if (Input.GetKeyDown(upgradeKey) && canAffordUpgrade)
        {
            BuyUpgrade();
        }
    }
    
    private void BuyUpgrade()
    {
        // Deduct points
        RoundManager.Instance.currentPoints -= upgradeCost;
        
        // Increase upgrade level
        upgradeLevel++;
        
        // Apply upgrades to gun
        ApplyUpgrades();
        
        Debug.Log($"Gun upgraded to level {upgradeLevel}!");
    }

    public void ApplyUpgrades()
    {
        if (gunController == null) return;
        
        // Increase damage
       
        
    }

    public int GetUpgradeLevel()
    {
        return upgradeLevel;
    }
}
