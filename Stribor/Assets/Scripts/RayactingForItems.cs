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

    private GameObject trenutniHideable;

    private GameObject Svarozic;

    private GameObject svarozicStation;

    LayerMask maskJelenice;

    LayerMask svarozicMask;

    LayerMask svarozicUpgradeMask;

    ParticleSystem upgradeSvarozica;

    public TMP_Text tooltips;

    public int brojJelenica;

    public GameObject vrijemeObjekt;

    private VrijemeSkripta vrijeme;


    public List<GameObject> ListaUpgradePointovaISistema; //Lista koja sadrzi objekte, objekt1 je stvar na koju ce igrac moci kliknuti, a index+1 je particle sistem za to
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
        svarozicSkripta = player.GetComponent<SvarozicGaming>();
        maskJelenice = LayerMask.GetMask("Jelenice");
        svarozicMask = LayerMask.GetMask("Svarozic");
        svarozicUpgradeMask = LayerMask.GetMask("SvarozicUpgrade");
        brojJelenica = 0;
        vrijeme = vrijemeObjekt.GetComponent<VrijemeSkripta>();
    }

    // Update is called once per frame

     void Update() {

        RaycastHit hit;

        //provjera za jelenice

        

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, maskJelenice)) {
            //skupi jelenicu
            //Debug.Log("Raycast hit");

            trenutnaJelenica = hit.transform.gameObject;

            //stvori outline jelenice
            hit.transform.gameObject.GetComponent<Outline>().enabled = true;

            Debug.DrawRay(ociLevel.position, Camera.main.transform.forward * hit.distance, Color.yellow, 2);

            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi jelenicu";

            tooltips.enabled = true;


            //promijeni boju, kasnije ce biti outline i dopustiti skupljanje

            if (Input.GetKeyDown(pickUpKey)) {

                brojJelenica += 1;
                JeleniceTekst.text = "Jelenice X " + brojJelenica;

                hit.collider.gameObject.SetActive(false);

                Debug.Log("Jelenica skupljena");

                tooltips.enabled = false;

                vrijeme.PromijeniVrijeme(brojJelenica);

            }

            


        } else if(trenutnaJelenica != null) {

            if (trenutnaJelenica.GetComponent<Outline>().enabled) {

                trenutnaJelenica.GetComponent<Outline>().enabled = false;
                tooltips.enabled = false;
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

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, svarozicMask)) {

            //udario le svarozica, skupi ga

            Svarozic = hit.transform.gameObject;

            Svarozic.GetComponent<Outline>().enabled = true;

            tooltips.enabled = true;
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi svarožića";

            if (Input.GetKeyDown(pickUpKey)) {

                svarozicSkripta.BrojSvarozica += 1;
                svarozicSkripta.SvaroziciTekst.text = "Svarozici: " + svarozicSkripta.BrojSvarozica;


                //UNISTENJE
                Destroy(Svarozic);

                Debug.Log("Svarozic skupljen");
                tooltips.enabled = false;

            }

        } else if(Svarozic != null) {

            if (Svarozic.GetComponent<Outline>().enabled) {

                Svarozic.GetComponent<Outline>().enabled = false;
                tooltips.enabled = false;
            }
        
        }

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, svarozicUpgradeMask)) {

            //stisce li igrac E

            svarozicStation = hit.transform.gameObject;

            svarozicStation.GetComponent<Outline>().enabled = true;

            tooltips.enabled = true;
            tooltips.text = "(" + pickUpKey.ToString() + ") Aktiviraj kamin";

            if (Input.GetKeyDown(pickUpKey) && !svarozicSkripta.SvarozicUgasen) {
                //nasao je neki upgrade station
                int indexStationa = ListaUpgradePointovaISistema.IndexOf(hit.transform.gameObject);

                //Lansiraj particle i upgradeaj svarozica i iskljuci kamin upgrade

                upgradeSvarozica = ListaUpgradePointovaISistema[indexStationa + 1].GetComponent<ParticleSystem>();

                hit.transform.gameObject.SetActive(false);

                upgradeSvarozica.Play();

                upgradeSvarozica.gameObject.GetComponent<AudioSource>().Play();

                svarozicSkripta.UpgradeSvarozic();

                tooltips.enabled = false;
                

            }
            




        } else if (svarozicStation != null) {
            if (svarozicStation.GetComponent<Outline>().enabled) {

                svarozicStation.GetComponent<Outline>().enabled = false;
                tooltips.enabled = false;
            }
        }

    }

    

}
