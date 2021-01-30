using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void RestartButton()
    {
        Time.timeScale = 1f;
        AutoRotate.moneyCount = 0;
        SceneManager.LoadScene("GameLevel");
    }

    public void StartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameLevel");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
