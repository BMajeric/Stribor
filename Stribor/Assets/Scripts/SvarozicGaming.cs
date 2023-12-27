using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SvarozicGaming : MonoBehaviour
{

    //SVAROZIC SVIJETLO STAGEOVI
    //STAGE 0: nemas ga lmao
    //STAGE 1: intensity: 15, range: 50
    //STAGE 2: intensity: 25, range: 75
    //STAGE 3: intensity: 45, range: 100
    //STAGE 3: intenstiy: 60, range = 150

    //koliko se mijenja t vrijednost izmedu svake korutine
    public float trast;

    //kolko brzo izmedu pokretanja korutina
    public float brzinaRasta;

    //za palit ili gasit svijetlo
    public KeyCode LightGumb = KeyCode.R;

    public List<GameObject> SvarozicSnage;

    private List<(float intenzitet, float domet)> ListaZaSvarozice = new List<(float, float)>(); //lista koja sprema intenzitete i range svarozica na pocetku igre

    public GameObject SvarozicURuci;

    private Light SvarozicSvijetlo;

    private bool MozeGasiti; //bool za onemogucavanje spemanja tijekom animacije

    public bool SvarozicUgasen; //bool za provjeru svijetla kod neprijatelja i tako

    public KeyCode BaciSvarozicaGumb = KeyCode.Mouse0;

    //koliko ih se moze bacati
    public int BrojSvarozica;

    public GameObject SvarozicPrefab;

    public Transform SvaroziciParent;

    //igrac ce moci ici do kamina da ojaca svijetlo svarozica
    public int snagaSvarozica = 1;

    public TMP_Text SvaroziciTekst;

    //lokacija s kojeg ce bacat svarozice
    public Vector3 startLokacija;

    //Snaga lansiranja
    public float LaunchForce;

    Vector3 smjerLansiranja;

    private float pocetniIntenzitet;

    // Start is called before the first frame update
    void Start()
    {
        //Pronadi zoom object igraca (eye level)
        startLokacija = GameObject.FindGameObjectWithTag("Player").transform.Find("ZoomObject").transform.position;

        LaunchForce = 40f;

        SvaroziciTekst.text = "Svarozici: " + BrojSvarozica;

        SvarozicURuci = SvarozicSnage[0];

        SvarozicSvijetlo = SvarozicURuci.transform.Find("Light").GetComponent<Light>();

        MozeGasiti = true;

        SvarozicUgasen = false;

        foreach (GameObject Svarozic in SvarozicSnage) {
            (float intenzitet, float domet) tuple;
            Light Svijetlo = Svarozic.transform.Find("Light").GetComponent<Light>();
            tuple.intenzitet = Svijetlo.intensity;
            tuple.domet = Svijetlo.range;
            ListaZaSvarozice.Add(tuple);
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(BaciSvarozicaGumb) && BrojSvarozica > 1 && SvarozicURuci.activeSelf) {
            LansirajSvarozica();
            UpgradeSvarozic();
            //gej
        }

        if (Input.GetKeyDown(LightGumb) && MozeGasiti && SvarozicURuci.activeSelf) {
            MozeGasiti = false;
            
            StartCoroutine(UgasiSvarozicaURuci(0f));
        } else if (Input.GetKeyDown(LightGumb) && MozeGasiti && !SvarozicURuci.activeSelf) {
            SvarozicURuci.SetActive(true);
            SvarozicUgasen = false;
            MozeGasiti = false;
            StartCoroutine(UpaliSvarozicaURuci(0f));
        }

        
    }

    void UpgradeSvarozic() {

        int index = SvarozicSnage.IndexOf(SvarozicURuci);

        if (index != SvarozicSnage.Count - 1) {

            //napravi neku animaciju
            SvarozicURuci.SetActive(false);
            SvarozicURuci = SvarozicSnage[index + 1];
            SvarozicURuci.SetActive(true);
            SvarozicSvijetlo = SvarozicURuci.transform.Find("Light").GetComponent<Light>();
        }

    }

    IEnumerator UgasiSvarozicaURuci(float t) {

        if (t == 0f) {
            int index = SvarozicSnage.IndexOf(SvarozicURuci);
            pocetniIntenzitet = ListaZaSvarozice[index].intenzitet;
        }

        t += trast;

        

        float lerpSvijetlo = Mathf.Lerp(pocetniIntenzitet, 0, t);

        

        SvarozicSvijetlo.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(UgasiSvarozicaURuci(t));
        } else {
            MozeGasiti = true;
            SvarozicUgasen = true;
            SvarozicURuci.SetActive(false);
        }

    }

    IEnumerator UpaliSvarozicaURuci(float t) {

        if (t == 0f) {
            int index = SvarozicSnage.IndexOf(SvarozicURuci);
            pocetniIntenzitet = ListaZaSvarozice[index].intenzitet;
        }

        t += trast;

        float lerpSvijetlo = Mathf.Lerp(0, pocetniIntenzitet, t);

        SvarozicSvijetlo.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(UpaliSvarozicaURuci(t));
        } else {
            MozeGasiti = true;
        }

    }

    void LansirajSvarozica() {
        Debug.Log("Lansiroje");

        startLokacija = SvarozicURuci.transform.position;

        GameObject SvarozicInWorld = Instantiate(SvarozicPrefab, SvaroziciParent);

        SvarozicInWorld.transform.position = startLokacija;

        int SvarozicLayer = LayerMask.NameToLayer("Svarozic");

        SvarozicInWorld.layer = SvarozicLayer;

        smjerLansiranja = Camera.main.transform.forward;

        SvarozicInWorld.GetComponent<Rigidbody>().AddForce(smjerLansiranja * LaunchForce);

        BrojSvarozica -= 1;

        SvaroziciTekst.text = "Svarozici: " + BrojSvarozica;

    }
}
