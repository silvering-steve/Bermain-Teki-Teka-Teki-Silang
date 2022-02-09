using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TTSButton1 : MonoBehaviour
{
    public string hurufJawab;
    public TextMeshProUGUI txtButton;

    public void setData(string _huruf)
    {
        hurufJawab = _huruf;
        txtButton.text = _huruf;
    }
}
