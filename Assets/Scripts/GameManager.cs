using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject controlsPanel;
    public GameObject levelSelectorPanel;
    
    private int collectibles;
    private int enemies;
    private int score;
    private bool win;

    public int Collectibles { get => collectibles; set => collectibles = value; }
    public int Enemies { get => enemies; set => enemies = value; }
    public int Score { get => score; set => score = value; }
    public bool Win { get => win; set => win = value; }

    private void Awake()
    {
        // First time
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ShowLevelSelector()
    {
        levelSelectorPanel.gameObject.SetActive(true);
    }

    public void HideLevelSelector()
    {
        levelSelectorPanel.gameObject.SetActive(false);
    }

    public void ShowControls()
    {
        controlsPanel.gameObject.SetActive(true);
    }

    public void HideControls()
    {
        controlsPanel.gameObject.SetActive(false);
    }
}
