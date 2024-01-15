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

    LayerMask vrataMask;

    private GameObject vrata;

    LayerMask jelenMask;

    private GameObject jelen;

    private GameObject item;

    LayerMask itemMask;


    public List<GameObject> ListaUpgradePointovaISistema; //Lista koja sadrzi objekte, objekt1 su stvaru na koju ce igrac moci kliknuti, a index+1 je particle sistem za to
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
        vrataMask = LayerMask.GetMask("Vrata");
        jelenMask = LayerMask.GetMask("Jelen");
        itemMask = LayerMask.GetMask("Item");

    }

    void HandleajIteme(GameObject hitObject) {

        hitObject.gameObject.GetComponent<Outline>().enabled = true;
        bool skupi = Input.GetKeyDown(pickUpKey);
        

        tooltips.enabled = true;

        switch(hitObject.tag) {
            
            case "SvarozicUpgrade":
            if (skupi) {
                int indexStationa = ListaUpgradePointovaISistema.IndexOf(hitObject);

                //Lansiraj particle i upgradeaj svarozica i iskljuci kamin upgrade

                upgradeSvarozica = ListaUpgradePointovaISistema[indexStationa + 1].GetComponent<ParticleSystem>();

                hitObject.transform.gameObject.SetActive(false);

                upgradeSvarozica.Play();

                upgradeSvarozica.gameObject.GetComponent<AudioSource>().Play();

                svarozicSkripta.UpgradeSvarozic();
            }
            tooltips.text = "(" + pickUpKey.ToString() + ") Aktiviraj kamin";
            break;
            
            case "Jelenica":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi jelenicu";
            if (skupi) {
                brojJelenica += 1;
                JeleniceTekst.text = "Jelenice X " + brojJelenica;

                hitObject.SetActive(false);

                Debug.Log("Jelenica skupljena");

                vrijeme.PromijeniVrijeme(brojJelenica);
            }
            break;

            case "Svarozic":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi svarožića";
            if (skupi) {
                svarozicSkripta.BrojSvarozica += 1;
                svarozicSkripta.SvaroziciTekst.text = "Svarozici: " + svarozicSkripta.BrojSvarozica;


                //UNISTENJE
                Destroy(hitObject);
                hitObject = null;

                Debug.Log("Svarozic skupljen");
            }
            break;

            case "VrataKamenolom":
            tooltips.text = "(" + pickUpKey.ToString() + ") Otvori vrata kamenoloma";
            if (skupi && ProstorEnums.striborProgress == ProstorEnums.StriborProgression.SkupioKljuc) {
                

                //otvori vrata kamenoloma
                hitObject.SetActive(false);
                
        
            }
            break;

            case "Jelen":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi jelena";
            if (skupi && ProstorEnums.striborProgress == ProstorEnums.StriborProgression.SkupioKljuc) {
                hitObject.SetActive(false);

                ProstorEnums.striborProgress = ProstorEnums.StriborProgression.SkupioJelena;
            }
            break;

            case "Hideable":
            tooltips.text = "(" + pickUpKey.ToString() + ") Sakrij se";
            if (skupi) {
                Debug.Log("Sakrio se buraz");
                hitObject.GetComponent<HideableObject>().Hide(player.transform);
            }
            break;

            case "Kljuc":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi ključ";
            if (skupi) {
                hitObject.SetActive(false);
                ProstorEnums.striborProgress = ProstorEnums.StriborProgression.SkupioKljuc;
            }
            break;

            default: //za sve Skupi stvari, jelenice, jelen, kljuc, svarozic...
                tooltips.text = "(" + pickUpKey.ToString() + ") Skupi " + hitObject.tag;
            break;

        }



    }

    // Update is called once per frame

     void Update() {
        

        RaycastHit hit;

        //provjera za iteme

        if (Physics.Raycast(ociLevel.position, Camera.main.transform.forward, out hit, 2, itemMask)) {
            Debug.DrawRay(ociLevel.position, Camera.main.transform.forward * hit.distance, Color.yellow, 2);
            item = hit.transform.gameObject;

            HandleajIteme(item);

        } else {
            
            if (item != null) {
                item.gameObject.GetComponent<Outline>().enabled = false;
                item = null;
                tooltips.enabled = false;
            }
            
        }



    }

    

}
