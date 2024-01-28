using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExposureManager : MonoBehaviour
{
    //handles exposure for the player

    //The player's exposure, determining how open he is to being chased. Exposure drops while hiding and rises while being outside or in the open
    public int Exposure;

    //the limit to which exposure will rise/fall to over time
    public int ExposureTarget;

    //how fast the player gains/loses exposure
    public float ExposureRate;

    //za hud i debug
    public TMP_Text ExposureText;

    //ako je igrac skriven u nekom grmu ili nesto -> skripta EnvironmentHide
    public bool environmentHidden;
    
    Dictionary<ProstorEnums.Lokacija,int> exposureDict = new Dictionary<ProstorEnums.Lokacija, int>();

    //igrac skripta za trackanje stanja

    FirstPersonController Player;

    SvarozicGaming svarozicSkripta;

    IEnumerator ChangeExposure() {

        Mathf.Clamp(Exposure, 0, 100);
        Mathf.Clamp(ExposureTarget, 0, 100);

        if (ExposureTarget < Exposure) {
            //lose exposure
            Exposure -= 1;
        } else {
            //gain exposure
            Exposure += 1;
        }

        ExposureText.text = "Exposure: " + Exposure;

        yield return new WaitForSeconds(ExposureRate);

        //provjeri je li exposure = exposureTarget i nastavi rutinu ako nije, pauziraj ako je

        if (Exposure == ExposureTarget) {

            yield return new WaitUntil(() => Exposure != ExposureTarget);

        }
        StartCoroutine("ChangeExposure");



    }

    //booloidi za usporedbu sa player booloidima
    bool Walk;
    bool Run;
    bool Crouch;
    bool Hiding;
    //zapravo kad god igrac nije grounded
    bool Grounded;

    bool environmentHiding;

    //je li svarozic ukljucen
    bool Svarozic;

    //kada igrac promijeni lokaciju postavi exposurerate
    public bool promijenioLokaciju;

    //exposure od lokacije u kojoj si
    int exposureOdLokacije;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>();
        svarozicSkripta = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<SvarozicGaming>();
        Walk = Player.isWalking;
        Run = Player.isSprinting;
        Crouch = Player.isCrouched;
        Grounded = Player.isGrounded;
        Svarozic = svarozicSkripta.SvarozicUgasen;
        promijenioLokaciju = false;
        exposureOdLokacije = 0;

        environmentHidden = false;
        environmentHiding = false;

        //definiraj dict
        exposureDict.Add(ProstorEnums.Lokacija.TamnaSuma, -30);
        exposureDict.Add(ProstorEnums.Lokacija.Poljana, 10);
        exposureDict.Add(ProstorEnums.Lokacija.Jezero, 10);
        exposureDict.Add(ProstorEnums.Lokacija.Kamenolom, 15);
        exposureDict.Add(ProstorEnums.Lokacija.Crkva, -5);
        exposureDict.Add(ProstorEnums.Lokacija.VelikoDrvo, -5);
        exposureDict.Add(ProstorEnums.Lokacija.Spilja, -10);
        exposureDict.Add(ProstorEnums.Lokacija.Brdo, 5);
        exposureDict.Add(ProstorEnums.Lokacija.LovackaKuca, -10);


        Exposure = 50;
        ExposureRate = 0.5f;
        ExposureTarget = 60;
        StartCoroutine("ChangeExposure");
        
    }

    // Update is called once per frame
    void Update()
    {
        //vuci varijable iz igraca i malo adjustaj exposure ovisno o kretnji igraca

        ModifyPlayerExposure();
        
        

    }

    void ModifyPlayerExposure() {

        if (Player.isWalking != Walk && Walk == false) {
            //Igrac je poceo hodati
            Walk = Player.isWalking;
            ExposureTarget += 5;
        } else if (Player.isWalking != Walk && Walk == true) {
            //Igrac je prestao hodati
            Walk = Player.isWalking;
            ExposureTarget -= 5;
        }

        if (Player.isCrouched != Crouch && Crouch == false) {
            //igrac je poceo crouchati
            Crouch = Player.isCrouched;
            ExposureTarget -= 20;
        } else if (Player.isCrouched != Crouch && Crouch == true) {
            //igrac je prestao crouchati
            Crouch = Player.isCrouched;
            ExposureTarget += 20;

        }

        if (Player.isSprinting != Run && Run == false) {
            //Igrac je poceo sprintati
            Run = Player.isSprinting;
            ExposureTarget += 15;
        } else if (Player.isSprinting != Run && Run == true) {
            //Igrac je prestao sprintati
            Run = Player.isSprinting;
            ExposureTarget -= 15;
        }

        if (Player.isGrounded != Grounded && Grounded == false) {
            //Igrac je u zraku
            
            Grounded = Player.isGrounded;
            ExposureTarget -= 10;
        } else if (Player.isGrounded != Grounded && Grounded == true) {
            //Igrac nije u zraku
            
            Grounded = Player.isGrounded;
            ExposureTarget += 10;
        }

        if (environmentHidden != environmentHiding && environmentHiding == false) {
            environmentHiding = environmentHidden;
            ExposureTarget -= 20;

        } else if (environmentHidden != environmentHiding && environmentHiding == true) {
            environmentHiding = environmentHidden;
            ExposureTarget += 20;
        }
        //Svarozic check
        if (svarozicSkripta.SvarozicUgasen != Svarozic && Svarozic == false) {
            //Igrac je u zraku
            
            Svarozic = svarozicSkripta.SvarozicUgasen;
            ExposureTarget -= 15;
        } else if (svarozicSkripta.SvarozicUgasen != Svarozic && Svarozic == true) {
            //Igrac nije u zraku
            
            Svarozic = svarozicSkripta.SvarozicUgasen;
            ExposureTarget += 15;
        }

        //lokacija stvar
        if (promijenioLokaciju) {

            
            
            promijenioLokaciju = false;

            ExposureTarget -= exposureOdLokacije;

            exposureOdLokacije = exposureDict[ProstorEnums.lokacijaIgraca];
            
            ExposureTarget += exposureOdLokacije;
            
        }
        

    }

}
