using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDeathPlane : MonoBehaviour
{
    //Skripta koja seta bool kada se igrac collidea sa triggerom

    PadanjeHandler padanjeHandler;
    RaycastingForItems raycasting;
    void Start()
    {
        padanjeHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PadanjeHandler>();
        raycasting = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>();
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            padanjeHandler.hitDeathPlane = true;
            raycasting.naTornju = false;
        }
    }

}
    
