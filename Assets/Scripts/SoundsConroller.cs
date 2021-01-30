using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsConroller : MonoBehaviour
{
    public AudioSource[] audioSources;

    private void Start()
    {

    }

    public void  GetCoin()
    {
        foreach (var source in audioSources)
            if (source.clip.name == "CoinCollect")
                source.Play();
    }
}

