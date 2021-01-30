using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHighScore : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = $"High score: {PlayerPrefs.GetInt("Score")}";
    }
}
