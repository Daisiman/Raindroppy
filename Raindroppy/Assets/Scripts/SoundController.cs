using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource boostSound;
    public AudioSource livesSound;

    void Start()
    {
        Debug.Log("Sound Controller loaded");
    }

    public void PlayBoost()
    {
        if (boostSound != null)
        {
            boostSound.Play();
        }
    }

    public void PlayLives()
    {
        if (livesSound != null)
        {
            livesSound.Play();
        }
    }
}
