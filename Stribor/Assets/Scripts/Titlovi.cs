using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Titlovi : MonoBehaviour
{
    SvarozicGaming svarozicSkripta;
    public float startTime;

    public KeyCode hint = KeyCode.H;

    public float automatskiHintTime = 120f;

    public float currentTime;

    public TextAsset titloviData;

    Dictionary<string, List<string>> bitneInformacije = new Dictionary<string, List<string>>(); //zapravo bitne informacije koje bi igrac morao cuti
    Dictionary<string, List<string>> korisneInformacije = new Dictionary<string, List<string>>(); //kinda korisne stvari koje mogu hintati stvari
    Dictionary<string, List<string>> nebitneInformacije = new Dictionary<string, List<string>>(); //lore, blebetanje i tako

    List<Dictionary<string, List<string>>> listaDictionarya = new List<Dictionary<string, List<string>>>();


    void popuniDict() {
        //funkicja sa SVIM LINEOVIMA
        string[] linije = File.ReadAllLines("./Assets/Scripts/Data/Titlovi.txt");

        listaDictionarya.Add(bitneInformacije);
        listaDictionarya.Add(korisneInformacije);
        listaDictionarya.Add(nebitneInformacije);

        int indexDict = 0;

        Dictionary<string, List<string>> trenutniDict = listaDictionarya[indexDict];

        string trenutniKljuc = "";

        bool sljedeciKljuc = false;

        foreach (string linija in linije) {
            //Debug.Log(linija);
            if (linija.Contains("/")) {
                //Debug.Log("skipara");
                continue;
            }

            if (linija == "") {
                //idi na sljedeci kljuc
                sljedeciKljuc = true;
                //Debug.Log("Sljedeci kljuc");
                continue;
            }

            if (linija.Contains("GOTOVO")) {
                //idi na sljedeci dikcionar
                indexDict += 1;
                trenutniDict = listaDictionarya[indexDict];
                continue;
            }

            if (sljedeciKljuc) {
                sljedeciKljuc = false;
                trenutniKljuc = linija;
                //Debug.Log(trenutniKljuc);
                trenutniDict.Add(trenutniKljuc, new List<string>());
                
                continue;
            }

            //zalijepi liniju u dict
            trenutniDict[trenutniKljuc].Add(linija);

        }

        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Subtitles.subtitles.typeSpeed = 0.1f;
        Subtitles.Show("Pocetak titlovanja", 8f, SubtitleEffect.Both, 20);

        svarozicSkripta = GameObject.FindGameObjectWithTag("Player").GetComponent<SvarozicGaming>();

        startTime = Time.time;

        popuniDict();
       
    }

    

    // Update is called once per frame
    void Update()
    {
        //svakih 2 minute ispali neki random voice line
        currentTime = Time.time - startTime;

        if (currentTime > automatskiHintTime) {
            //ispali neki hint
        }

        //ispali voice line ako stisnes H

        if (Input.GetKeyDown(hint)) {
            //Ispali neki random voice line
        }
    }



}
