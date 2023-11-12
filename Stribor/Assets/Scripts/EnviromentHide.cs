using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class EnviromentHide : MonoBehaviour
{
     FirstPersonController playerController;

     BoxCollider hideCollider;

     ExposureManager exposureManager;

    // Start is called before the first frame update
    void Start()
    {
        hideCollider = this.GetComponent<BoxCollider>();

        exposureManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ExposureManager>();

    }

    
    //ako je igrac u collideru
    void OnTriggerStay(Collider other)
    {

        //other je player

        if (hideCollider.bounds.Contains(other.bounds.max) && hideCollider.bounds.Contains(other.bounds.min) && other.gameObject.tag == "Player") {

            //Player je u potpunosti u grmu ili sta vec pa smanji exposure rate

            exposureManager.environmentHidden = true;

        }
        
    }


    
    void OnTriggerExit(Collider other)
    {
        //igrac odjebao
        if (other.gameObject.tag == "Player") {
            Debug.Log("Odjebaus");
            exposureManager.environmentHidden = false;
        }

        
    }

    
}
