using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public string sceneName;

    private bool isPaused = false;
    public void SettingBtn()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume time
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Stop time
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneName);
    }

}
