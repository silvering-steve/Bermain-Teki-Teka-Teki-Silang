using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTSManager1 : MonoBehaviour
{
    [System.Serializable]
    public class SoalTTS
    {
        public string soal;
        public string noSoal;
        public List<TTSKotak1> listKotakJawab;
    }

    public class ListSoalTTS
    {
        public List<SoalTTS> listSoal;
    }

    [Header("Soal")] public List<SoalTTS> listSoal;

    [Space(order = 3)] public int indexSoal = 0;
    public TextMeshProUGUI soalTxt;
    public TextMeshProUGUI soalNo;

    [Header("Jawab")] public List<TTSKotak1> kotakAll;
    public List<TTSKotak1> kotakSebagian;
    public TTSKotak1 kotakSelected;

    [Header("Button")] public GameObject btnJawab;
    public Transform parentJawab;

    [Space(order = 3)] public List<GameObject> jawabList;
    public Image check;
    public Image solved;

    [Header("Info")] public GameObject info;
    public List<Sprite> imgList;

    public static System.Random rnd = new System.Random();

    private List<string> satuD;
    private List<string> duaD;
    private List<string> tigaD;
    private List<string> satuDtemp;
    private List<string> duaDtemp;
    private List<string> tigaDtemp;

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

        satuD = new List<string> { };
        duaD = new List<string> { };
        tigaD = new List<string> { };
        satuDtemp = new List<string> {"R", "S", "E", "I"};
        duaDtemp = new List<string> {"T", "A", "R", "I", "N", "G"};
        tigaDtemp = new List<string> {"G", "E", "R", "A", "H", "M"};

        // Add random

        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 8; j++)
                {
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    satuDtemp.Add(huruf[shuffleNum]);
                }
            }
            else if (i == 1)
            {
                for (int j = 0; j < 6; j++)
                {
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    duaDtemp.Add(huruf[shuffleNum]);
                }
            }
            else if (i == 2)
            {
                for (int j = 0; j < 6; j++)
                {
                    int shuffleNum = rnd.Next(0, (huruf.Count));
                    tigaDtemp.Add(huruf[shuffleNum]);
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    int shuffleNum = rnd.Next(0, (satuDtemp.Count));
                    satuD.Add(satuDtemp[shuffleNum]);
                    satuDtemp.Remove(satuDtemp[shuffleNum]);
                }
            }
            else if (i == 1)
            {
                for (int j = 0; j < 12; j++)
                {
                    int shuffleNum = rnd.Next(0, (duaDtemp.Count));
                    duaD.Add(duaDtemp[shuffleNum]);
                    duaDtemp.Remove(duaDtemp[shuffleNum]);
                }
            }
            else if (i == 2)
            {
                for (int j = 0; j < 12; j++)
                {
                    int shuffleNum = rnd.Next(0, (tigaDtemp.Count));
                    tigaD.Add(tigaDtemp[shuffleNum]);
                    tigaDtemp.Remove(tigaDtemp[shuffleNum]);
                }
            }
        }
    }

    private void Update()
    {
        Done();
        True();
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
            solved.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            solved.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void True()
    {
        bool benar = true;

        // Check question answer is true

        SoalTTS _soal = listSoal[indexSoal];
        foreach (var a in _soal.listKotakJawab)
        {
            if (!a.isRight)
            {
                benar = false;
                break;
            }
        }

        if (benar)
        {
            check.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            check.GetComponentInChildren<Button>().interactable = false;
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

    public void ChangeImage()
    {
        info.GetComponentInChildren<Image>().sprite = imgList[indexSoal];

        info.SetActive(true);
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

        if (indexSoal == 0)
        {
            foreach (var b in satuD)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 1)
        {
            foreach (var b in duaD)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 2)
        {
            foreach (var b in tigaD)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
    }
}
