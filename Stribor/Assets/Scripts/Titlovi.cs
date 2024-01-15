using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Titlovi : MonoBehaviour
{
    SvarozicGaming svarozicSkripta;
    public float startTime;

    public KeyCode hint = KeyCode.H;

    public float automatskiHintTime = 120f;

    public float currentTime;

    Dictionary<string, List<string>> bitneInformacije = new Dictionary<string, List<string>>(); //zapravo bitne informacije koje bi igrac morao cuti
    Dictionary<string, List<string>> korisneInformacije = new Dictionary<string, List<string>>(); //kinda korisne stvari koje mogu hintati stvari
    Dictionary<string, List<string>> nebitneInformacije = new Dictionary<string, List<string>>(); //lore, blebetanje i tako

    List<Dictionary<string, List<string>>> listaDictionarya = new List<Dictionary<string, List<string>>>();

    public List<string> bitniUkljuceniTitlovi = new List<string>(); //ove linije ce se izreci samo jednom, sa prioritetom iznad ostalih i trebalo bi ih zapisati u neki notes da ih igrac ima

    public List<string> ukljuceniTitlovi = new List<string>(); //ostali titlovi, korisni ilki nebitni koji ce se random vaditi kada nema bitnih

    public List<string> biljeske = new List<string>(); //korisni titlovi ce biti zapisani u biljeske za kasnije gledanje

    List<string> iskoristeniBitniKljucevi = new List<string>(); //iskoristeni kljucevi bitnih informacija da se ne ponavljaju

    float cooldown = 10f; //cooldown za hintovea

    float cooldownStart; 


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


    public void ukljuciTitlove(string kljuc) {
        //bitne informacije ukljuci samo ako ih nisi vec prije ukljucio
        if (bitneInformacije.ContainsKey(kljuc)) {
            //dodaj sve stvari u listu
            if (!iskoristeniBitniKljucevi.Contains(kljuc)) {
                bitniUkljuceniTitlovi.AddRange(bitneInformacije[kljuc]);
                iskoristeniBitniKljucevi.Add(kljuc);
                Debug.Log("Dodao " + kljuc + " bitne informacije");
            }
            
        }

        if (korisneInformacije.ContainsKey(kljuc)) {
            Debug.Log("Dodao " + kljuc + " korisne informacije");
            foreach (string linija in korisneInformacije[kljuc]) {
                ukljuceniTitlovi.Add(linija);
            }
        }

        if (nebitneInformacije.ContainsKey(kljuc)) {
            Debug.Log("Dodao " + kljuc + " nebitne informacije");
            foreach (string linija in nebitneInformacije[kljuc]) {
                ukljuceniTitlovi.Add(linija);
            }
        }

    }

    public void iskljuciTitlove(string kljuc) {

        //bitne informacije se ne micu
         

        if (korisneInformacije.ContainsKey(kljuc)) {
            
            foreach (string linija in korisneInformacije[kljuc]) {
                ukljuceniTitlovi.Remove(linija);
            }
        }

        if (nebitneInformacije.ContainsKey(kljuc)) {
            
            foreach (string linija in nebitneInformacije[kljuc]) {
                ukljuceniTitlovi.Remove(linija);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Subtitles.subtitles.typeSpeed = 0.1f;
        Subtitles.Show("Pocetak titlovanja", 8f, SubtitleEffect.Both, 25);

        svarozicSkripta = GameObject.FindGameObjectWithTag("Player").GetComponent<SvarozicGaming>();

        startTime = Time.time;

        cooldownStart = Time.time;

        popuniDict();
       
    }

    

    // Update is called once per frame
    void Update()
    {
        //svakih 2 minute ispali neki random voice line
        currentTime = Time.time - startTime;

        if (currentTime > automatskiHintTime && !svarozicSkripta.SvarozicUgasen) {
            //ispali neki hint iz korisnih i nebitnih hintova
            if (ukljuceniTitlovi.Count > 0) {
                int random = Random.Range(0, ukljuceniTitlovi.Count);
                string odabraniTitl = ukljuceniTitlovi[random];

                startTime = Time.time;

                Subtitles.Show(odabraniTitl, 5f, SubtitleEffect.Both, 25);

            } else {
                startTime = Time.time;
            }
        }

        //ispali voice line ako stisnes H

        if (Input.GetKeyDown(hint) && !svarozicSkripta.SvarozicUgasen && Time.time - cooldownStart > cooldown) {
            //Ispali neki random voice line, korisni ako ih ima
            if (bitniUkljuceniTitlovi.Count > 0) {
                int random = Random.Range(0, bitniUkljuceniTitlovi.Count);
                string odabraniTitl = bitniUkljuceniTitlovi[random];

                startTime = Time.time;

                Subtitles.Show(odabraniTitl, 5f, SubtitleEffect.Both, 25);
                //makni titl iz liste i zapisi ga u biljeske
                bitniUkljuceniTitlovi.Remove(odabraniTitl);
                biljeske.Add(odabraniTitl);


            } else if (ukljuceniTitlovi.Count > 0) {
                int random = Random.Range(0, ukljuceniTitlovi.Count);
                string odabraniTitl = ukljuceniTitlovi[random];

                startTime = Time.time;

                Subtitles.Show(odabraniTitl, 5f, SubtitleEffect.Both, 25);
            } else {
                Subtitles.Show("Nemam ništa za reći, idemo dalje.", 5f, SubtitleEffect.Both, 25);
            }
            cooldownStart = Time.time;
        }
    }



}
