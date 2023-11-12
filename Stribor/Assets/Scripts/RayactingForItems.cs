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

    private GameObject trenutnaJelenica;

    private GameObject trenutniHideable;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame

     void Update() {

        RaycastHit hit;

        //provjera za jelenice
        LayerMask maskJelenice = LayerMask.GetMask("Jelenice");

        

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, maskJelenice)) {
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

        //provjera za hideable objekte

        LayerMask maskHideable = LayerMask.GetMask("Hideable");

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 4, maskHideable)) {

            //pogodio je hideable sada se treba onak sakriti

            trenutniHideable = hit.transform.gameObject;

            //stvori outline stvari
            hit.transform.gameObject.GetComponent<Outline>().enabled = true;

            if (Input.GetKeyDown(pickUpKey)) {

                Debug.Log("Sakrio se buraz");
                trenutniHideable.GetComponent<HideableObject>().Hide(player.transform);
            }

        } else if(trenutniHideable != null) {

            if (trenutniHideable.GetComponent<Outline>().enabled) {

                trenutniHideable.GetComponent<Outline>().enabled = false;
            }

            
        }

        
    }
}
