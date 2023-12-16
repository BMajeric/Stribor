using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SvarozicGaming : MonoBehaviour
{

    //za palit ili gasit svijetlo
    public KeyCode LightGumb = KeyCode.R;

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


        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(BaciSvarozicaGumb) && BrojSvarozica > 1) {
            LansirajSvarozica();
        }

        
    }

    void LansirajSvarozica() {
        Debug.Log("Lansiroje");

        startLokacija = GameObject.FindGameObjectWithTag("Player").transform.Find("SvarozicURuci").transform.position;

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
