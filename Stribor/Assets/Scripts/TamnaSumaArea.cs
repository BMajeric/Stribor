using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamnaSumaArea : MonoBehaviour
{

    // Update is called once per frame
    
    //Default vrijednosti magle Boja = 575757 hexadecimal, RGB = 0.3396226, 0.3396226, 0.3396226 fog density = 0.02, mod = exponential squared

    FirstPersonController playerScript;

    Color pocetnaBojaMagle;

    private void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        pocetnaBojaMagle.r = 0.3396226f;
        pocetnaBojaMagle.g = 0.3396226f;
        pocetnaBojaMagle.b = 0.3396226f;

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            Debug.Log("Igrac usao u sumu");

            //RenderSettings.fogDensity = 0.5f;

            //RenderSettings.fogColor = Color.black;

            //sada povecaj fog i onemoguci kretanje

            playerScript.enableSprint = false;

            playerScript.fov = 50;

            playerScript.walkSpeed = 3;

            StartCoroutine(PromijeniMaglu(0f));

        }
    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {



        
            Debug.Log("Igrac izasao iz sume");

            playerScript.enableSprint = true;

            playerScript.walkSpeed = 5;

            playerScript.fov = 70;

            //RenderSettings.fogDensity = 0.02f;

            //RenderSettings.fogColor = pocetnaBojaMagle;

            //sada smanji fog i omoguci kretanje

            StartCoroutine(OdmijeniMaglu(0f));
        }
        
    }

    IEnumerator PromijeniMaglu(float t) {

        t += 0.05f;

        Color lerpedColor = Color.Lerp(pocetnaBojaMagle, Color.black, t);

        float lerpedDensity = Mathf.Lerp(0.02f, 0.5f, t);

        RenderSettings.fogColor = lerpedColor;

        RenderSettings.fogDensity = lerpedDensity;

        yield return new WaitForSeconds(0.1f);

        if (t < 1f) {
            StartCoroutine(PromijeniMaglu(t));
        }

    }

    IEnumerator OdmijeniMaglu(float t) {

        t += 0.05f;

        Color lerpedColor = Color.Lerp(Color.black, pocetnaBojaMagle, t);

        float lerpedDensity = Mathf.Lerp(0.5f, 0.02f, t);

        RenderSettings.fogColor = lerpedColor;

        RenderSettings.fogDensity = lerpedDensity;

        yield return new WaitForSeconds(0.1f);

        if (t < 1f) {
            StartCoroutine(OdmijeniMaglu(t));
        }

    }

}
