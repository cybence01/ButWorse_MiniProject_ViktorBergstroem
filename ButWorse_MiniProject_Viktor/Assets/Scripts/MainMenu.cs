using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Settings")]
    // Make sure this matches the exact name of your game scene
    [SerializeField] private string gameSceneName = "GameScene"; 

    private void Start()
    {
        // Ensure correct state on start
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        
        // Unlock mouse cursor so player can click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        // Loads the scene by name
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        // Hide menu, show options
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        // Hide options, show menu
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}