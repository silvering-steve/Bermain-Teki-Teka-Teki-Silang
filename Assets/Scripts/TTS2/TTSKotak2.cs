using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TTSKotak2 : MonoBehaviour, IPointerClickHandler
{
    public string jawabTemp;
    public string jawabBenar;
    
    public TextMeshProUGUI jawabTxt;
    public TTSManager2 ttsManager;

    public bool isRight;
    public bool canClick;

    private void Start()
    {
        jawabTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        
        ttsManager.ResetSelected();
        GetComponent<Image>().color = Color.red;
        ttsManager.kotakSelected = this;
    }

    public void SetJawab(string _jawaban)
    {
        _jawaban.ToUpper();

        jawabTemp = _jawaban.ToUpper();
        jawabTxt.text = _jawaban.ToUpper();

        if (_jawaban.ToUpper() == jawabBenar)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
    }

    public void DeleteAnswer()
    {
        jawabTemp = " ";
        jawabTxt.text = " ";
        isRight = false;
    }

    public void Disable()
    {
        canClick = false;
        GetComponent<Image>().color = Color.grey;
    }

    public void Enable()
    {
        canClick = true;
        GetComponent<Image>().color = Color.white;
    }
}
