using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycastingForItems : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private FirstPersonController playerController;

    private SvarozicGaming svarozicSkripta;

    public Transform ociLevel;

    public KeyCode pickUpKey = KeyCode.E;

    public TMP_Text JeleniceTekst;

    private GameObject trenutnaJelenica;

    private GameObject Svarozic;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
        svarozicSkripta = player.GetComponent<SvarozicGaming>();
    }


     void Update() {

        RaycastHit hit;

        //provjera za jelenice
        LayerMask mask = LayerMask.GetMask("Jelenice");

        LayerMask svarozicMask = LayerMask.GetMask("Svarozic");

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, mask)) {
            //skupi jelenicu
            //Debug.Log("Raycast hit");

            trenutnaJelenica = hit.transform.gameObject;

            //stvori outline jelenice
            hit.transform.gameObject.GetComponent<Outline>().enabled = true;

            Debug.DrawRay(ociLevel.position, Camera.main.transform.forward * hit.distance, Color.yellow, 2);


            //promijeni boju, kasnije ce biti outline i dopustiti skupljanje

            if (Input.GetKeyDown(pickUpKey)) {

                playerController.BrojJelenica += 1;
                JeleniceTekst.text = "Jelenice X " + playerController.BrojJelenica;

                hit.collider.gameObject.SetActive(false);

                Debug.Log("Jelenica skupljena");

            }

            


        } else if(trenutnaJelenica != null) {

            if (trenutnaJelenica.GetComponent<Outline>().enabled) {

                trenutnaJelenica.GetComponent<Outline>().enabled = false;
            }

            
        }

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, svarozicMask)) {

            //udario le svarozica, skupi ga

            Svarozic = hit.transform.gameObject;

            Svarozic.GetComponent<Outline>().enabled = true;

            if (Input.GetKeyDown(pickUpKey)) {

                svarozicSkripta.BrojSvarozica += 1;
                svarozicSkripta.SvaroziciTekst.text = "Svarozici: " + svarozicSkripta.BrojSvarozica;


                //UNISTENJE
                Destroy(Svarozic);

                Debug.Log("Svarozic skupljen");

            }

        } else if(Svarozic != null) {

            if (Svarozic.GetComponent<Outline>().enabled) {

                Svarozic.GetComponent<Outline>().enabled = false;
            }
        
        }
    }

}
