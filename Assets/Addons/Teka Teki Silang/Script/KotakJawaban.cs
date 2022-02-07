using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class KotakJawaban : MonoBehaviour, IPointerClickHandler
{
    public string tampung_jawaban;
    public string jawaban_benar;
    public ttsmanager tts_manager;
    public TextMeshProUGUI txtJawaban;
    public bool sudah_benar = false;
    public bool canClick;

    public void Start() {
        txtJawaban = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!canClick){
            return;
        }
        tts_manager.Reset_Selected();
        GetComponent<Image>().color = Color.red;
        tts_manager.kotak_jawabanSelected = this;
    }
    public void SetJawaban(string _jawaban){
        _jawaban.ToUpper();
        tampung_jawaban = _jawaban.ToUpper();
        txtJawaban.text = _jawaban.ToUpper();
        if(_jawaban.ToUpper() == jawaban_benar){
            sudah_benar = true;
        }else{
            sudah_benar = false;
        }
    }

    public void DeleteJawaban(){
        tampung_jawaban = "";
        txtJawaban.text = "";
        sudah_benar = false;
    }
    public void Disable(){
        canClick = false;
        GetComponent<Image>().color = Color.grey;
    }
    public void Enable(){
        canClick = true;
        GetComponent<Image>().color = Color.white;
    }
}
