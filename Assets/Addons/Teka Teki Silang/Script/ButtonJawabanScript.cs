using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonJawabanScript : MonoBehaviour
{
    public string hurufJawaban;
    public TextMeshProUGUI txtButton;
    public void SetData(string _huruf){
        hurufJawaban = _huruf;
        txtButton.text = _huruf;
    }
}
