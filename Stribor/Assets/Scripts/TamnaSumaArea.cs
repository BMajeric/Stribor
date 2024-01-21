using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.Universal;

public class TamnaSumaArea : MonoBehaviour
{

    // Update is called once per frame
    
    //Default vrijednosti magle Boja = 575757 hexadecimal, RGB = 0.3396226, 0.3396226, 0.3396226 fog density = 0.02, mod = exponential squared

    FirstPersonController playerScript;

    SvarozicGaming svarozicSkripta;

    public bool checkSvarozic; //debug da ne moras imat max svarozica

    Color pocetnaBojaMagle;

    private Volume volume;

    private VolumeProfile profil;

    private Vignette vinjeta;

    public bool vani;

    public float trast;

    public float brzinaRasta;

    private void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        svarozicSkripta = GameObject.FindGameObjectWithTag("Player").GetComponent<SvarozicGaming>();
        
        pocetnaBojaMagle.r = 0.3396226f;
        pocetnaBojaMagle.g = 0.3396226f;
        pocetnaBojaMagle.b = 0.3396226f;

        volume = GameObject.Find("Global Volume").GetComponent<Volume>();

        profil = volume.sharedProfile;

        

        profil.TryGet(out vinjeta);
        
        vinjeta.intensity.overrideState = true;

        vani = false;
        

        

    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            Debug.Log("Igrac usao u sumu");

            this.GetComponent<TamnaSumaEnemy>().enabled = true;

            //RenderSettings.fogDensity = 0.5f;

            //RenderSettings.fogColor = Color.black;


            //sada povecaj fog i onemoguci kretanje

            playerScript.enableSprint = false;

            playerScript.fov = 50;

            playerScript.walkSpeed = 3;

            StartCoroutine(PromijeniMaglu(0f));

            int index = svarozicSkripta.SvarozicSnage.IndexOf(svarozicSkripta.SvarozicURuci);

            if (index != svarozicSkripta.SvarozicSnage.Count - 1) {

                if (checkSvarozic) {
                    Subtitles.Show("Nisam dovoljno jak za tamnu šumu, ajmo pobjeći prije nego nas uhvati!", 5f, SubtitleEffect.Both, 25);
                
                    StartCoroutine(UbijIgracaBezSvijetla(0f));
                }
                //pocni ubijat igraca
                

            }

        }
    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {

            this.GetComponent<TamnaSumaEnemy>().enabled = false;

        
            Debug.Log("Igrac izasao iz sume");

            playerScript.enableSprint = true;

            playerScript.walkSpeed = 5;

            playerScript.fov = 70;

            //RenderSettings.fogDensity = 0.02f;

            //RenderSettings.fogColor = pocetnaBojaMagle;

            //sada smanji fog i omoguci kretanje

            StartCoroutine(OdmijeniMaglu(0f));

            int index = svarozicSkripta.SvarozicSnage.IndexOf(svarozicSkripta.SvarozicURuci);

            if (index != svarozicSkripta.SvarozicSnage.Count - 1) {

                //pocni ubijat igraca
                if (checkSvarozic) {
                    StartCoroutine(OdUbijIgracaBezSvijetla(0f));
                }
                

            }
        }
        
    }

    IEnumerator PromijeniMaglu(float t) {
        vani = true;
        t += trast;

        Color lerpedColor = Color.Lerp(pocetnaBojaMagle, Color.black, t);

        float lerpedDensity = Mathf.Lerp(0.02f, 0.5f, t);

        RenderSettings.fogColor = lerpedColor;

        RenderSettings.fogDensity = lerpedDensity;

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(PromijeniMaglu(t));
        }

    }

    IEnumerator OdmijeniMaglu(float t) {
        vani = false;
        t += trast;

        Color lerpedColor = Color.Lerp(Color.black, pocetnaBojaMagle, t);

        float lerpedDensity = Mathf.Lerp(0.5f, 0.02f, t);

        RenderSettings.fogColor = lerpedColor;

        RenderSettings.fogDensity = lerpedDensity;

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(OdmijeniMaglu(t));
        }

    }

    IEnumerator UbijIgracaBezSvijetla(float t) {
        //ubi igraca ako nema svarozica level 4
        
        t += 0.01f;

        float lerpedVignette = Mathf.Lerp(0.25f, 1f, t);


        vinjeta.intensity.value = lerpedVignette;

        yield return new WaitForSeconds(0.1f);


        if (t < 1f && vani) {
            StartCoroutine(UbijIgracaBezSvijetla(t));
        } else {
            //ubi igraca
            Debug.Log("Smrt");
        }

    }

    IEnumerator OdUbijIgracaBezSvijetla(float t) {
        //makni vinjetu ako izade iz sume
        
        t += 0.1f;

        float lerpedVignette = Mathf.Lerp(1f, 0.25f, t);


        vinjeta.intensity.value = lerpedVignette;

        yield return new WaitForSeconds(0.1f);


        if (t < 1f) {
            StartCoroutine(OdUbijIgracaBezSvijetla(t));
        } else {
            //ubi igraca
            Debug.Log("Jooo nisi umro");
        }

    }

}
