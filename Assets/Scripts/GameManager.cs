using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject controlsPanel;
    
    private int collectibles;
    
    public int Collectibles { get => collectibles; set => collectibles = value; }

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

    public void ShowControls()
    {
        controlsPanel.gameObject.SetActive(true);
    }

    public void HideControls()
    {
        controlsPanel.gameObject.SetActive(false);
    }
}
