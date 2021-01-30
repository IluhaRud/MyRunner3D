using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject text;

    public void PlayButton()
    {
        SceneManager.LoadScene("GameLevel");
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void MainMenuButton()
    {
        AutoRotate.moneyCount = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
