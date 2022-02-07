using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System.Linq;

public class ttsmanager : MonoBehaviour
{
    [System.Serializable]
    public class SoalTekaTeki{
        public string soal;
        public string hint_soal;
        public List<KotakJawaban> list_kotakJawabannya;
    }

    [Header("Ini Soal")]
    public List<SoalTekaTeki> list_soal;

    [Space(order = 5)]
    public int indexSoal = 0;
    public TextMeshProUGUI txtSoal;
    public TextMeshProUGUI txtHintSoal;
    public List<KotakJawaban> list_kotakJawaban;
    public List<KotakJawaban> list_kotak;
    public KotakJawaban kotak_jawabanSelected;

    public GameObject prefab_btnJawaban;
    public Transform parent_btnJawaban;

    public List<GameObject> list_jawabanTertampil;
    public Image checkJawaban;
    public Image solved;

    public static System.Random rnd = new System.Random();
    List<string> satuD;
    List<string> duaD;
    List<string> tigaD;
    List<string> empatD;
    List<string> satuT;
    List<string> duaT;
    List<string> tigaT;
    List<string> satuDtemp;
    List<string> duaDtemp;
    List<string> tigaDtemp;
    List<string> empatDtemp;
    List<string> satuTtemp;
    List<string> duaTtemp;
    List<string> tigaTtemp;

    private void Awake() {
        List<string> alfabhet = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
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
        for(int i = 0; i < 7; i++){
            if(i == 0){
                for(int j = 0; j < 8; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    satuDtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 1){
                for(int j = 0; j < 6; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    duaDtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 2){
                for(int j = 0; j < 4; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    tigaDtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 3){
                for(int j = 0; j < 6; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    empatDtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 4){
                for(int j = 0; j < 7; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    satuTtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 5){
                for(int j = 0; j < 7; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    duaTtemp.Add(alfabhet[shuffleNum]);
                }
            }else if(i == 6){
                for(int j = 0; j < 4; j++){
                    int shuffleNum = rnd.Next(0, (alfabhet.Count));
                    tigaTtemp.Add(alfabhet[shuffleNum]);
                }
            }
        }

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
        CheckIfItsDone();
        CheckEach();
    }

    public void CheckIfItsDone(){
        bool done = true;
        foreach(var a in list_soal){
            foreach(var b in list_kotakJawaban){
                if(!b.sudah_benar){
                    done = false;
                    break;
                }
            }
        }
        if(done){
            solved.color = Color.green;
        }else{
            solved.color = Color.red;
        }
    }

    public void CheckEach(){
        bool check = true;
        SoalTekaTeki _soal = list_soal[indexSoal];
        foreach(var a in _soal.list_kotakJawabannya){
            if(!a.sudah_benar){
                check = false;
                break;
            }
        }
        if(check){
            checkJawaban.GetComponentInChildren<Button>().interactable = true;
        }else{
            checkJawaban.GetComponentInChildren<Button>().interactable = false;
        }
    }
    private void Start(){
        foreach(var a in list_kotakJawaban){
            a.tts_manager = this;
        }
        GenerateJawabanDanSoal();
    }
    public void Reset_Selected(){
        foreach (var a in list_kotakJawaban.Where(x=>x.canClick).ToList())
        {
            a.GetComponent<Image>().color = Color.white;
        }
    }
    public void NextSoal(){
        if(indexSoal == list_soal.Count - 1){
            indexSoal = 0;
        }else{
            indexSoal++;
        }
        foreach (var a in list_jawabanTertampil)
            Destroy(a);
        list_jawabanTertampil.Clear();

        GenerateJawabanDanSoal();
    }
    public void BeforeSoal(){
        if(indexSoal == 0){
            indexSoal = list_soal.Count - 1;
        }else{
            indexSoal--;
        }
        foreach (var a in list_jawabanTertampil)
            Destroy(a);
        list_jawabanTertampil.Clear();

        GenerateJawabanDanSoal();
    }

    public void Hapus(){
        kotak_jawabanSelected.DeleteJawaban();
    }
    public void GenerateJawabanDanSoal(){
        foreach (var a in list_soal){
            foreach(var b in a.list_kotakJawabannya){
                b.Disable();
            }
        }
        SoalTekaTeki _soal = list_soal[indexSoal];
        txtSoal.text = _soal.soal;
        txtHintSoal.text = _soal.hint_soal;
        foreach(var a in _soal.list_kotakJawabannya){
            a.Enable();
        }
        if(indexSoal == 0){
            foreach(var b in satuD){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 1){
            foreach(var b in duaD){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 2){
            foreach(var b in tigaD){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 3){
            foreach(var b in empatD){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 4){
            foreach(var b in satuT){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 5){
            foreach(var b in duaT){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }else if(indexSoal == 6){
            foreach(var b in tigaT){
                GameObject _btnJawaban = Instantiate(prefab_btnJawaban, parent_btnJawaban);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotak_jawabanSelected != null){
                        kotak_jawabanSelected.SetJawaban(b);
                    }
                });
                list_jawabanTertampil.Add(_btnJawaban);
            }
        }
    }
}