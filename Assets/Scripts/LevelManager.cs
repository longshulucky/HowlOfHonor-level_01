using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int monstersToKill = 10;
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private Button mainMenuButton;

    private int monstersKilled = 0;

    void Start()
    {
        levelCompleteCanvas.SetActive(false);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    public void OnMonsterKilled()
    {
        monstersKilled++;

        if (monstersKilled >= monstersToKill)
        {
            ShowLevelCompleteCanvas();
        }
    }

    void ShowLevelCompleteCanvas()
    {
        levelCompleteCanvas.SetActive(true);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
