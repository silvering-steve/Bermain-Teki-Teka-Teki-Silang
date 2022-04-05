using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTSManager5 : MonoBehaviour
{
    [System.Serializable]
    public class SoalTTS
    {
        public bool playAudio;
        public string soal;
        public string noSoal;
        public List<TTSKotak5> listKotakJawab;
    }

    [Header("Soal")] 
    public List<SoalTTS> listSoal;

    [Space(order = 3)] 
    public int indexSoal = 0;
    public int indexJawaban = 0;
    public TextMeshProUGUI soalTxt;
    public TextMeshProUGUI soalNo;

    [Header("Jawab")] 
    public List<TTSKotak5> kotakAll;
    public List<TTSKotak5> kotakSebagian;
    public TTSKotak5 kotakSelected;

    [Header("Button")] 
    public GameObject btnJawab;
    public Transform parentJawab;

    [Space(order = 3)] 
    public List<GameObject> jawabList;
    public Image check;
    public Image solved;

    [Header("Info")]
    public GameObject info;
    public List<Sprite> imgList;

    public static System.Random rnd = new System.Random();

    private List<string> satuD;
    private List<string> duaD;
    private List<string> tigaD;
    private List<string> empatD;
    private List<string> limaD;
    private List<string> enamD;
    private List<string> tujuhD;
    private List<string> satuDtemp;
    private List<string> duaDtemp;
    private List<string> tigaDtemp;
    private List<string> empatDtemp;
    private List<string> limaDtemp;
    private List<string> enamDtemp;
    private List<string> tujuhDtemp;

    List<string> huruf = new List<string>
    {
        "Q", "W", "E", "R", "T",
        "Y", "U", "I", "O", "P",
        "A", "S", "D", "F", "G",
        "H", "J", "K", "L", "Z",
        "X", "C", "V", "B", "N", "M"
    };

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
            if (!listSoal[indexSoal].playAudio)
            {
                GameManager.PlaySound1();
                listSoal[indexSoal].playAudio = true;
            }
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
        kotakSelected = null;
        indexJawaban = 0;
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
        kotakSelected = null;
    }

    public void Hapus()
    {
        SoalTTS _soal = listSoal[indexSoal];
        if (kotakSelected == null)
        {
            if (indexJawaban == 0)
            {
                kotakSelected = _soal.listKotakJawab[_soal.listKotakJawab.Count() - 1];
                kotakSelected.DeleteAnswer();
                indexJawaban = _soal.listKotakJawab.Count() - 1;
                kotakSelected = null;
            }
            else
            {
                kotakSelected = _soal.listKotakJawab[indexJawaban - 1];
                kotakSelected.DeleteAnswer();
                indexJawaban -= 1;
                kotakSelected = null;
            }
        }
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
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        // foreach (var a in _soal.listKotakJawab)
                        // {
                        //     if (kotakSelected)
                        //     {
                        //         a.GetComponent<Image>().color = Color.green;
                        //     }
                        // }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 1)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 2)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 3)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 4)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 5)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 6)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 7)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 8)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 9)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 10)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
        else if (indexSoal == 11)
        {
            foreach (var b in huruf)
            {
                GameObject _btnJawaban = Instantiate(btnJawab, parentJawab);
                _btnJawaban.GetComponent<ButtonJawabanScript>().SetData(b);
                _btnJawaban.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (kotakSelected == null)
                    {
                        kotakSelected = _soal.listKotakJawab[indexJawaban];
                        // kotakSelected.GetComponent<Image>().color = Color.red;

                        foreach (var a in _soal.listKotakJawab)
                        {
                            if (!kotakSelected)
                            {
                                a.GetComponent<Image>().color = Color.white;
                            }
                        }
                        
                        kotakSelected.SetJawab(b);
                        GameManager.PlaySound3();
                        indexJawaban++;
                        kotakSelected = null;

                        if (indexJawaban == _soal.listKotakJawab.Count())
                        {
                            indexJawaban = 0;
                        }
                    } 
                    else if (kotakSelected != null)
                    {
                        kotakSelected.SetJawab(b);
                    }
                });
                jawabList.Add(_btnJawaban);
            }
        }
    }
}
