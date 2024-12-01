using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Hunter hunter;
    [SerializeField] private bool isPaused = false;

    void Update()
    {
        if (!gameOverMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            } else PauseGame();
        }
        if (hunter.GetHealth() <= 0)
        {
            GameOver();
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void TryAgain()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
