using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] GameObject pauseMenuUI;

    [Header("Settings")]
    [SerializeField] string mainMenuSceneName = "MainMenu";

    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
