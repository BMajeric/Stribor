using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SvarozicGaming : MonoBehaviour
{

    //SVAROZIC SVIJETLO STAGEOVI
    //STAGE 0: nemas ga lmao
    //STAGE 1: intensity: 15, range: 50, flashlight: intenstiy: 75, range = 100
    //STAGE 2: intensity: 25, range: 75, flashlight: intenstiy: 100, range = 100
    //STAGE 3: intensity: 45, range: 100, flashlight: intenstiy: 225, range = 100
    //STAGE 4: intenstiy: 60, range = 150, flashlight: intenstiy: 300, range = 100

    //koliko se mijenja t vrijednost izmedu svake korutine
    public float trast;

    //kolko brzo izmedu pokretanja korutina
    public float brzinaRasta;

    public float trastFlash;

    public float brzinaGasenjaSvijetla;

    public float brzinaRastaFlash;

    //za palit ili gasit svijetlo
    public KeyCode LightGumb = KeyCode.R;

    public KeyCode promijeniModGumb = KeyCode.Mouse1;

    public List<GameObject> SvarozicSnage;

    public List<GameObject> SvaroziciBacanje;

    private List<(float pointIntenzitet, float pointDomet, float flashIntenzitet, float flashDomet)> ListaZaSvarozice = new List<(float, float, float, float)>(); //lista koja sprema intenzitete i range svarozica na pocetku igre

    public GameObject SvarozicURuci;

    private Light SvarozicSvijetlo;

    private Light SvarozicFlashlight;

    private bool MozeGasiti; //bool za onemogucavanje spemanja tijekom animacije

    public bool SvarozicUgasen; //bool za provjeru svijetla kod neprijatelja i tako

    public bool MozeMijenjatiMode; //moze li igrac mijenjati mode

    public KeyCode BaciSvarozicaGumb = KeyCode.Mouse0;

    //koliko ih se moze bacati
    public int BrojSvarozica;

    public GameObject SvarozicPrefab;

    public Transform SvaroziciParent;

    //igrac ce moci ici do kamina da ojaca svijetlo svarozica
    public int snagaSvarozica = 0;

    public TMP_Text SvaroziciTekst;

    //lokacija s kojeg ce bacat svarozice
    public Vector3 startLokacija;

    //Snaga lansiranja
    public float LaunchForce;

    Vector3 smjerLansiranja;

    private float pocetniIntenzitet;

    private float pocetniIntenzitetFlash;

    public bool gasenjeBaterije;

    public bool svarozicUgasenTijekFlashmodea;

    private float zavrsniIntenzitetFlash;

    public AudioSource audioSource;

    public AudioClip throwSound;

    public Mode svarozicMode;

    public enum Mode {
        Point,

        Flashlight,
    }


    // Start is called before the first frame update
    void Start()
    {
        //Pronadi zoom object igraca (eye level)
        startLokacija = GameObject.FindGameObjectWithTag("Player").transform.Find("ZoomObject").transform.position;

        LaunchForce = 40f;

        SvaroziciTekst.text = "Domaci: " + BrojSvarozica;

        SvarozicPrefab = SvaroziciBacanje[0];

        SvarozicSvijetlo = SvarozicURuci.transform.Find("Light").GetComponent<Light>();

        SvarozicFlashlight = SvarozicURuci.transform.Find("FlashLight").GetComponent<Light>();

        MozeGasiti = true;

        SvarozicUgasen = false;

        svarozicMode = Mode.Point;

        ListaZaSvarozice.Add((15f, 50f, 75f, 100f));
        ListaZaSvarozice.Add((25f, 75f, 100f, 100f));
        ListaZaSvarozice.Add((45f, 100f, 225f, 100f));
        ListaZaSvarozice.Add((60f, 150f, 300f, 100f));

        SvarozicSvijetlo.intensity = ListaZaSvarozice[snagaSvarozica].pointIntenzitet;
        SvarozicSvijetlo.range = ListaZaSvarozice[snagaSvarozica].pointDomet;

        SvarozicFlashlight.intensity = ListaZaSvarozice[snagaSvarozica].flashIntenzitet;
        SvarozicFlashlight.range = ListaZaSvarozice[snagaSvarozica].flashDomet;

        gasenjeBaterije = false;

        svarozicUgasenTijekFlashmodea = false;

        MozeMijenjatiMode = true;

        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(BaciSvarozicaGumb) && BrojSvarozica > 1 && SvarozicURuci.activeSelf) {
            LansirajSvarozica();
            //UpgradeSvarozic();
            //gej
        }

        if (Input.GetKeyDown(LightGumb) && MozeGasiti && SvarozicURuci.activeSelf) {
            MozeGasiti = false;
            
            MozeMijenjatiMode = false;

            if (svarozicMode == Mode.Point) {
                StartCoroutine(UgasiSvarozicaURuci(0f));
            } else if (svarozicMode == Mode.Flashlight) {
                trastFlash *= 5;
                svarozicUgasenTijekFlashmodea = true;
            }


        } else if (Input.GetKeyDown(LightGumb) && MozeGasiti && !SvarozicURuci.activeSelf) {
            SvarozicURuci.SetActive(true);
            SvarozicUgasen = false;
            MozeGasiti = false;
            MozeMijenjatiMode = false;

            
            StartCoroutine(UpaliSvarozicaURuci(0f));
             

            
        }

        if (Input.GetKeyDown(promijeniModGumb) && MozeMijenjatiMode) {
            //animacija i promijeni
            //Debug.Log("Promjena svijetla");
            gasenjeBaterije = false;
            PromijeniMode();
        }

        if (svarozicMode == Mode.Flashlight) {
            //rotiraj svarozica da prati crosshair nekako
            //Debug.Log(Camera.main.transform.localRotation.x);
    
            SvarozicURuci.transform.localRotation = Quaternion.Euler(new Vector3(Camera.main.transform.localRotation.eulerAngles.x, 210f, 0f));

            //neki smooth movement??

            //Vector3 moveTowardsCamera = Vector3.MoveTowards(SvarozicURuci.transform.localRotation.eulerAngles, Camera.main.transform.localRotation.eulerAngles, 20f);

            //SvarozicURuci.transform.localRotation = Quaternion.Euler(moveTowardsCamera.x, 210f, 0f);
        }

        if (svarozicMode == Mode.Flashlight && gasenjeBaterije == false) {
            gasenjeBaterije = true;
            Debug.Log("Gasim bateriju");
            StartCoroutine(GasiSnaguBaterije(0f));

        }

        
    }

    public void PromijeniMode() {
        //promijeni svarozica u flashlight mode, protresi i cao bao


        if (svarozicMode == Mode.Point) 
        {
            //MozeGasiti = true;
            svarozicMode = Mode.Flashlight;
            SvarozicSvijetlo.gameObject.SetActive(false);
            SvarozicFlashlight.gameObject.SetActive(true);
            SvarozicURuci.transform.localRotation = Quaternion.Euler(new Vector3(0f, 210f, 0f));


        } 
        else if (svarozicMode == Mode.Flashlight) 
        {
            //MozeGasiti = false;
            svarozicMode = Mode.Point;
            SvarozicSvijetlo.gameObject.SetActive(true);
            SvarozicFlashlight.gameObject.SetActive(false);
            SvarozicURuci.transform.localRotation = Quaternion.Euler(new Vector3(0f, 80f, 0f));
        }



    }

    IEnumerator GasiSnaguBaterije(float t) {

        //preko 10 sekundi smanjuj intenzitet svijetla i na kraju promijeni mod

        //provjeri mod baterije i prekini korutinu ako je point

        if (svarozicMode == Mode.Point) {
            SvarozicFlashlight.intensity = pocetniIntenzitetFlash;
            yield break;

        }

        if (t == 0f) {
            pocetniIntenzitetFlash = ListaZaSvarozice[snagaSvarozica].flashIntenzitet;
            zavrsniIntenzitetFlash = ListaZaSvarozice[snagaSvarozica].pointIntenzitet;
        }

        t += trastFlash;

        float lerpSvijetlo = Mathf.Lerp(pocetniIntenzitetFlash, zavrsniIntenzitetFlash, t);

        SvarozicFlashlight.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(brzinaGasenjaSvijetla);

        if (t < 1f) {
            StartCoroutine(GasiSnaguBaterije(t));
        } else {
            //promijeni mod
            PromijeniMode();
            gasenjeBaterije = false;

            if (svarozicUgasenTijekFlashmodea) {
                svarozicUgasenTijekFlashmodea = false;
                trastFlash /= 5;
                StartCoroutine(UgasiSvarozicaURuci(0f));
            }
        }



    }

    public void UpgradeSvarozic() {

        int index = SvarozicSnage.IndexOf(SvarozicURuci);

        if (snagaSvarozica != 3) {

            //napravi neku animaciju
            snagaSvarozica += 1;
            
            SvarozicSvijetlo.intensity = ListaZaSvarozice[snagaSvarozica].pointIntenzitet;
            SvarozicSvijetlo.range = ListaZaSvarozice[snagaSvarozica].pointDomet;

            SvarozicFlashlight.intensity = ListaZaSvarozice[snagaSvarozica].flashIntenzitet;
            SvarozicFlashlight.range = ListaZaSvarozice[snagaSvarozica].flashDomet;
        }

    }

    IEnumerator UgasiSvarozicaURuci(float t) {

        if (t == 0f) {
            int index = SvarozicSnage.IndexOf(SvarozicURuci);
            pocetniIntenzitet = ListaZaSvarozice[snagaSvarozica].pointIntenzitet;
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
            MozeMijenjatiMode = false;
            SvarozicURuci.SetActive(false);
        }

    }

    IEnumerator UpaliSvarozicaURuci(float t) {

        if (t == 0f) {
            int index = SvarozicSnage.IndexOf(SvarozicURuci);
            pocetniIntenzitet = ListaZaSvarozice[snagaSvarozica].pointIntenzitet;
        }

        t += trast;

        float lerpSvijetlo = Mathf.Lerp(0, pocetniIntenzitet, t);

        SvarozicSvijetlo.intensity = lerpSvijetlo;

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(UpaliSvarozicaURuci(t));
        } else {
            MozeGasiti = true;
            MozeMijenjatiMode = true;
        }

    }

    void LansirajSvarozica() {
        Debug.Log("Lansiroje");

        startLokacija = SvarozicURuci.transform.position;

        GameObject SvarozicInWorld = Instantiate(SvarozicPrefab, SvaroziciParent);

        SvarozicInWorld.transform.position = startLokacija;

        int itemMask = LayerMask.NameToLayer("Item");

        SvarozicInWorld.layer = itemMask;

        smjerLansiranja = Camera.main.transform.forward;

        SvarozicInWorld.GetComponent<Rigidbody>().AddForce(smjerLansiranja * LaunchForce);

        BrojSvarozica -= 1;

        SvaroziciTekst.text = "Domaći: " + BrojSvarozica;

        audioSource.PlayOneShot(throwSound);

    }
}
