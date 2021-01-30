using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoRotate : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, 48, 0);

    public static int moneyCount = 0;

    public Text scoreText;

    public SoundsConroller audio;

    private void Start()
    {
        scoreText = GameObject.Find("[UIController]").transform.Find("ScoreText").GetComponent<Text>();
        audio = GameObject.Find("[SoundController]").GetComponent<SoundsConroller>();
    }

    void Update()
    {
        transform.Rotate(
             speed.x * Time.deltaTime,
             speed.y * Time.deltaTime,
             speed.z * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            moneyCount++;
            scoreText.text = $"Money: {moneyCount}";
            audio.GetCoin();
            Destroy(gameObject);
        }
    }
}