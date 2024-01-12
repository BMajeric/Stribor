using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class LokacijaSkripta : MonoBehaviour
{
    //Skripta za definiranje lokacije igraca na ulasku u prostor

    public ProstorEnums.Lokacija lokacijaSkripte;

    ExposureManager exposureManager;

    private void Start() {
        exposureManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ExposureManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Usao " + lokacijaSkripte);
        ProstorEnums.lokacijaIgraca = lokacijaSkripte;
        exposureManager.promijenioLokaciju = true;
    }
    
}
