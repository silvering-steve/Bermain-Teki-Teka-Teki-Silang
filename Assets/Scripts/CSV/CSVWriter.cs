using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    private string _fileName = "";
    private bool _isFile;
    
    public TMP_InputField nama;
    public TMP_InputField kelas;
    public TMP_InputField sekolah;

    [System.Serializable]
    public class Murid
    {
        public string name;
        public string kelas;
        public string sekolah;
    }

    [System.Serializable]
    public class ListMurid
    {
        public Murid[] murid;
    }

    public ListMurid muridnya = new ListMurid();
    
    void Start()
    {
        _fileName = Application.persistentDataPath + "/dataSiswa.csv";
        _isFile = File.Exists(Application.persistentDataPath + "/dataSiswa.csv");

        if (_isFile)
        {
            Debug.Log("File Found");
            Debug.Log(Application.persistentDataPath);
        }
        else
        {
            TextWriter tw = new StreamWriter(_fileName, false);
            tw.WriteLine("Nama, Kelas, Sekolah");
            tw.Close();

            Debug.Log("File Created");
        }
    }

    public void WriteCSV()
    {
        string namanya = nama.text;
        string kelasnya = kelas.text;
        string sekolahnya = sekolah.text;
        
        TextWriter tw = new StreamWriter(_fileName, true);
        tw.WriteLine(namanya + "," + kelasnya + "," + sekolahnya);
        tw.Close();
    }
}
