using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class VrijemeSkripta : MonoBehaviour
{
    RaycastingForItems raycastingForItems;

    public Transform sunce;
    // Start is called before the first frame update
    void Start()
    {
        raycastingForItems = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>();
        
    }

    public void PromijeniVrijeme(int brojJelenica) {

        if (brojJelenica == 2 && ProstorEnums.smrtIgraca == ProstorEnums.Smrt.UmroJednom) {
            //promijeni na 45 stupnjeva
            sunce.rotation = Quaternion.Euler(45f, 0f, 0f);
        } else if (brojJelenica == 7) {
            sunce.rotation = Quaternion.Euler(25f, 0f, 0f);
        } else if (brojJelenica == 10) {
            sunce.rotation = Quaternion.Euler(0f, 0f, 0f);
        } else if (brojJelenica == 12) {
            sunce.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }

    }
}
