using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI livesTxt;
    public TextMeshProUGUI collectiblesTxt;
    public TextMeshProUGUI enemiesTxt;
    public TextMeshProUGUI timeTxt;

    public void SetTimeTxt(int levelTime)
    {
        int mins = (int)levelTime / 60;
        int secs = (int)levelTime % 60;
        timeTxt.text = mins.ToString("Time 00" + ":") + secs.ToString("00");
    }

    public void SetLivesTxt(int lives)
    {
        livesTxt.text = "Lives: " + lives.ToString();
    }

    public void SetCollectiblesTxt(int collectibles)
    {
        collectiblesTxt.text = "Acorns: " + collectibles.ToString();
    }

    public void SetEnemiesTxt(int enemies)
    {
        enemiesTxt.text = "Enemies: " + enemies.ToString();
    }

    public void SetTimesUpBox()
    {
        canvas.transform.Find("Panel").gameObject.SetActive(false);
        canvas.transform.Find("TimesUpPanel").gameObject.SetActive(true);
    }

    public void SetWinBox()
    {
        canvas.transform.Find("WinPanel").gameObject.SetActive(true);
    }

    public void SetLoseLivesBox()
    {
        canvas.transform.Find("Panel").gameObject.SetActive(false);
        canvas.transform.Find("TimesUpPanel").gameObject.SetActive(false);
        canvas.transform.Find("LosePanel").gameObject.SetActive(true);
    }
}