using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycastingForItems : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private FirstPersonController playerController;

    public Transform ociLevel;

    public KeyCode pickUpKey = KeyCode.E;

    public TMP_Text JeleniceTekst;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void FixedUpdate() {

        RaycastHit hit;

        //provjera za jelenice
        LayerMask mask = LayerMask.GetMask("Jelenice");

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, mask)) {
            //skupi jelenicu
            //Debug.Log("Raycast hit");

            Debug.DrawRay(ociLevel.position, Camera.main.transform.forward * hit.distance, Color.yellow, 2);


            //promijeni boju, kasnije ce biti outline i dopustiti skupljanje

            if (Input.GetKeyDown(pickUpKey)) {

                playerController.BrojJelenica += 1;
                JeleniceTekst.text = "Jelenice X " + playerController.BrojJelenica;

                hit.collider.gameObject.SetActive(false);

                Debug.Log("Jelenica skupljena");

            }

            


        }
        
    }
}
