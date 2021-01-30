using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Obstacles : MonoBehaviour
{
    AudioSource crashSound;

    private void Start()
    {
        crashSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Find("[UIController]").transform.Find("GameOverPanel").gameObject.SetActive(true);
        GameObject.Find("[UIController]").transform.Find("GameOverPanel").Find("ScoreText").GetComponent<Text>().text = $"Total score: {AutoRotate.moneyCount}";

        if (PlayerPrefs.GetInt("Score") < AutoRotate.moneyCount)
            PlayerPrefs.SetInt("Score", AutoRotate.moneyCount);

        crashSound.Play();
        Time.timeScale = 0f;
    }
}
