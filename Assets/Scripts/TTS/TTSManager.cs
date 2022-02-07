using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTSManager : MonoBehaviour
{
    [System.Serializable]

    public class SoalTTS
    {
        public string soal;
        public string noSoal;
        public List<TTSKotak> listKotakJawab;
    }

    [Header("Soal")] 
    public List<SoalTTS> listSoal;

    [Space(order = 3)]
    public int indexSoal = 0;
    public TextMeshProUGUI soalTxt;
    public TextMeshProUGUI soalNo;

    [Header("Jawab")] 
    public List<TTSKotak> kotakAll;
    public List<TTSKotak> kotakSebagian;
    public TTSKotak kotakSelected;

    [Header("Button")] 
    public GameObject btnJawab;
    public Transform parentJawab;

    [Space(order = 3)]
    public List<GameObject> jawabList;
    public Image check;
    public Image solved;
    
    public static System.Random rnd = new System.Random();

    private List<string> satuD;
    private List<string> duaD;
    private List<string> tigaD;
    private List<string> empatD;
    private List<string> satuT;
    private List<string> duaT;
    private List<string> tigaT;
    private List<string> satuDtemp;
    private List<string> duaDtemp;
    private List<string> tigaDtemp;
    private List<string> empatDtemp;
    private List<string> satuTtemp;
    private List<string> duaTtemp;
    private List<string> tigaTtemp;

    private void Awake()
    {
        List<string> huruf = new List<string>
        {
            "A", "B", "C", "D", "E", 
            "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", 
            "P", "Q", "R", "S", "T", 
            "U", "V", "W", "X", "Y", "Z"
        };
        
        satuD = new List<string> {};
        duaD = new List<string> {};
        tigaD = new List<string> {};
        empatD = new List<string> {};
        satuT = new List<string> {};
        duaT = new List<string> {};
        tigaT = new List<string> {};
        satuDtemp = new List<string> {"P", "A", "S", "T"};
        duaDtemp = new List<string> {"L", "E", "M", "B", "U", "T"};
        tigaDtemp = new List<string> {"K", "E", "P", "A", "L", "C", "I", "L"};
        empatDtemp = new List<string> {"B", "E", "N", "I", "G", "A"};
        satuTtemp = new List<string> {"G", "E", "A", "L", "S"};
        duaTtemp = new List<string> {"S", "I", "K", "A", "T"};
        tigaTtemp = new List<string> {"F", "L", "U", "O", "R", "I", "D", "E"};
        
        // Add random
        
        for(int i = 0; i < 7; i++){
            if(i == 0){
                for(int j = 0; j < 8; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    satuDtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 1){
                for(int j = 0; j < 6; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    duaDtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 2){
                for(int j = 0; j < 4; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    tigaDtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 3){
                for(int j = 0; j < 6; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    empatDtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 4){
                for(int j = 0; j < 7; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    satuTtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 5){
                for(int j = 0; j < 7; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    duaTtemp.Add(huruf[shuffleNum]);
                }
            }else if(i == 6){
                for(int j = 0; j < 4; j++){
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    tigaTtemp.Add(huruf[shuffleNum]);
                }
            }
        }

        // Shuffle the alphabet
        
        for(int i = 0; i < 7; i++){
            if(i == 0){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (satuDtemp.Count));
                    satuD.Add(satuDtemp[shuffleNum]);
                    satuDtemp.Remove(satuDtemp[shuffleNum]);
                }
            }else if(i == 1){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (duaDtemp.Count));
                    duaD.Add(duaDtemp[shuffleNum]);
                    duaDtemp.Remove(duaDtemp[shuffleNum]);
                }
            }else if(i == 2){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (tigaDtemp.Count));
                    tigaD.Add(tigaDtemp[shuffleNum]);
                    tigaDtemp.Remove(tigaDtemp[shuffleNum]);
                }
            }else if(i == 3){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (empatDtemp.Count));
                    empatD.Add(empatDtemp[shuffleNum]);
                    empatDtemp.Remove(empatDtemp[shuffleNum]);
                }
            }else if(i == 4){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (satuTtemp.Count));
                    satuT.Add(satuTtemp[shuffleNum]);
                    satuTtemp.Remove(satuTtemp[shuffleNum]);
                }
            }else if(i == 5){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (duaTtemp.Count));
                    duaT.Add(duaTtemp[shuffleNum]);
                    duaTtemp.Remove(duaTtemp[shuffleNum]);
                }
            }else if(i == 6){
                for(int j = 0; j < 12; j++){
                    int shuffleNum = rnd.Next(0, (tigaTtemp.Count));
                    tigaT.Add(tigaTtemp[shuffleNum]);
                    tigaTtemp.Remove(tigaTtemp[shuffleNum]);
                }
            }
        }
    }
    
    private void Update() {
       
    }

    public void Done()
    {
        bool done = true;
        
        // Check if all already answered
        
        foreach (var a in listSoal)
        {
            foreach (var b in kotakAll)
            {
                if (!b.isRight)
                {
                    done = false;
                    break;
                }
            }
        }

        if (done)
        {
            solved.color = Color.green;
        }
        else
        {
            solved.color = Color.red;
        }
    }

    public void True()
    {
        bool check = true;

        // Check question answer is true
        
        SoalTTS _soal = listSoal[indexSoal];
        foreach (var a in _soal.listKotakJawab)
        {
            if (a!.isRight)
            {
                check = false;
                break;
            }
        }

        if (check)
        {
            this.check.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            this.check.GetComponentInChildren<Button>().interactable = false;
        }
    }

    private void Start()
    {
        foreach (var a in kotakAll)
        {
            a.ttsManager = this;
        }
        
        GenerateSoal();
    }

    public void ResetSelected()
    {
        foreach (var a in kotakAll.Where(x => x.canClick).ToList())
        {
            a.GetComponent<Image>().color = Color.white;
        }
    }

    public void NextSoal()
    {
        if (indexSoal == listSoal.Count - 1)
        {
            indexSoal = 0;
        }
        else
        {

            indexSoal++;
        }

        foreach (var a in jawabList)
            Destroy(a);
        
        jawabList.Clear();
        
        GenerateSoal();
        
    }

    public void BeforeSoal()
    {
        if (indexSoal == 0)
        {
            indexSoal = listSoal.Count - 1;
        }
        else
        {
            indexSoal--;
        }

        foreach (var a in jawabList)
            Destroy(a);

        jawabList.Clear();
        
        GenerateSoal();
    }
    
    public void Hapus()
    {
        kotakSelected.DeleteAnswer();
    }
    
    public void GenerateSoal()
    {
        foreach (var a in listSoal)
        {
            foreach (var b in kotakAll)
            {
                b.Disable();
            }
        }

        SoalTTS _soal = listSoal[indexSoal];
        soalTxt.text = _soal.soal;
        soalNo.text = _soal.noSoal;

        foreach (var a in _soal.listKotakJawab)
        {
            a.Enable();
        }
        
        if(indexSoal == 0){
            foreach(var b in satuD){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 1){
            foreach(var b in duaD){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 2){
            foreach(var b in tigaD){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 3){
            foreach(var b in empatD){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 4){
            foreach(var b in satuT){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 5){
            foreach(var b in duaT){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }else if(indexSoal == 6){
            foreach(var b in tigaT){
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null){
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
    }
}
