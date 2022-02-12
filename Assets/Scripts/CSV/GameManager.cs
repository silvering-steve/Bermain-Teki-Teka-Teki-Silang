using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject audioSource;

    public static AudioClip correctSound;
    
    public static AudioSource audioCorrect;
    public static AudioSource audioDone;
    public static AudioSource audioButton;

    private void Start()
    {
        audioButton = audioSource.transform.Find("Audio Source 2").GetComponent<AudioSource>();
        audioCorrect = audioSource.transform.Find("Audio Source 3").GetComponent<AudioSource>();
        audioDone = audioSource.transform.Find("Audio Source 4").GetComponent<AudioSource>(); 
        
        correctSound = audioSource.transform.Find("Audio Source 3").GetComponent<AudioSource>().clip;
    }

    public static void PlaySound1()
    {
        audioCorrect.PlayOneShot(correctSound);
    }

    public static void PlaySound2()
    {
        audioDone.Play();
    }

    public static void PlaySound3()
    {
        audioButton.Play();
    }
    
    public void ExitApp()
    {
        Application.Quit();
    }
}

