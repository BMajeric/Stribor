using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SvarozicGaming : MonoBehaviour
{

    //SVAROZIC SVIJETLO STAGEOVI
    //STAGE 0: nemas ga lmao
    //STAGE 1: intensity: 15, range: 100
    //STAGE 2: intensity: 35, range: 100
    //STAGE 3: intenstiy: 60, range = 150

    //za palit ili gasit svijetlo
    public KeyCode LightGumb = KeyCode.R;

    public GameObject SvarozicURuci;

    private Light SvarozicSvijetlo;

    private bool MozeGasiti; //bool za onemogucavanje spemanja tijekom animacije

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

    // Start is called before the first frame update
    void Start()
    {
        //Pronadi zoom object igraca (eye level)
        startLokacija = GameObject.FindGameObjectWithTag("Player").transform.Find("ZoomObject").transform.position;

        LaunchForce = 40f;

        SvaroziciTekst.text = "Svarozici: " + BrojSvarozica;

        SvarozicSvijetlo = SvarozicURuci.transform.Find("Light").GetComponent<Light>();

        MozeGasiti = true;


        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(BaciSvarozicaGumb) && BrojSvarozica > 1 && SvarozicURuci.activeSelf) {
            LansirajSvarozica();
        }

        if (Input.GetKeyDown(LightGumb) && MozeGasiti && SvarozicURuci.activeSelf) {
            MozeGasiti = false;
            StartCoroutine(UgasiSvarozicaURuci(0f));
        } else if (Input.GetKeyDown(LightGumb) && MozeGasiti && !SvarozicURuci.activeSelf) {
            SvarozicURuci.SetActive(true);
            MozeGasiti = false;
            StartCoroutine(UpaliSvarozicaURuci(0f));
        }

        
    }

    IEnumerator UgasiSvarozicaURuci(float t) {

        t += 0.1f;

        float lerpSvijetlo = Mathf.Lerp(30, 0, t);

        SvarozicSvijetlo.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(0.1f);

        if (t < 1f) {
            StartCoroutine(UgasiSvarozicaURuci(t));
        } else {
            MozeGasiti = true;
            SvarozicURuci.SetActive(false);
        }

    }

    IEnumerator UpaliSvarozicaURuci(float t) {

        t += 0.1f;

        float lerpSvijetlo = Mathf.Lerp(0, 30, t);

        SvarozicSvijetlo.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(0.1f);

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
