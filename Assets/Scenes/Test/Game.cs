using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Game : MonoBehaviour
{
    public static int lives = 3;
    public static int sekor;
    public GameObject canvas;
    
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;

    private void Update()
    {
        Dead();
        text.text = "Lives : " + lives;
        text2.text = "Score : " + sekor;
    }

    private void Dead()
    {
        if (lives == 0)
        {
            canvas.SetActive(true);
        }
    }
}
