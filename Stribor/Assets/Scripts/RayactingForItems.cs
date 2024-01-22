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

    ParticleSystem upgradeSvarozica;

    public TMP_Text tooltips;

    public int brojJelenica;

    public GameObject vrijemeObjekt;

    private VrijemeSkripta vrijeme;

    private GameObject item;

    LayerMask itemMask;

    Titlovi titlovi;

    public GameObject svarozicURuci;

    public Subtitles subtitlesSkripta;

    public AudioSource itemSound;

    public AudioClip pickUo;

    public AudioClip hide;

    public Animator vrata;


    public List<GameObject> ListaUpgradePointovaISistema; //Lista koja sadrzi objekte, objekt1 su stvaru na koju ce igrac moci kliknuti, a index+1 je particle sistem za to
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
        svarozicSkripta = player.GetComponent<SvarozicGaming>();
        brojJelenica = 0;
        vrijeme = vrijemeObjekt.GetComponent<VrijemeSkripta>();
        itemMask = LayerMask.GetMask("Item");
        titlovi = GameObject.FindGameObjectWithTag("Titlovi").GetComponent<Titlovi>();
        

    }

    void HandleajIteme(GameObject hitObject) {

        hitObject.gameObject.GetComponent<Outline>().enabled = true;
        bool skupi = Input.GetKeyDown(pickUpKey);
        

        tooltips.enabled = true;
        Debug.Log(hitObject.tag);

        switch(hitObject.tag) {
            
            case "SvarozicUpgrade":
            if (skupi) {
                int indexStationa = ListaUpgradePointovaISistema.IndexOf(hitObject);

                //Lansiraj particle i upgradeaj svarozica i iskljuci kamin upgrade

                upgradeSvarozica = ListaUpgradePointovaISistema[indexStationa + 1].GetComponent<ParticleSystem>();

                hitObject.transform.gameObject.SetActive(false);

                //odredi koji state imas
                switch(ProstorEnums.svaroziciUpgradeTracker) {

                    case ProstorEnums.SvaroziciUpgrade.BezUpgradea:
                    ProstorEnums.svaroziciUpgradeTracker = ProstorEnums.SvaroziciUpgrade.NakonPrvogUpgradea;
                    break;

                    case ProstorEnums.SvaroziciUpgrade.NakonPrvogUpgradea:
                    ProstorEnums.svaroziciUpgradeTracker = ProstorEnums.SvaroziciUpgrade.NakonDrugogUpgradea;
                    break;

                    case ProstorEnums.SvaroziciUpgrade.NakonDrugogUpgradea:
                    ProstorEnums.svaroziciUpgradeTracker = ProstorEnums.SvaroziciUpgrade.NakonTrecegUpgradea;
                    break;

                }

                



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
                itemSound.PlayOneShot(pickUo);

                hitObject.SetActive(false);

                Debug.Log("Jelenica skupljena");

                vrijeme.PromijeniVrijeme(brojJelenica);
            }
            break;

            case "Svarozic":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi svarožića";
            if (skupi) {
                svarozicSkripta.BrojSvarozica += 1;
                svarozicSkripta.SvaroziciTekst.text = "Domaći: " + svarozicSkripta.BrojSvarozica;
                itemSound.PlayOneShot(pickUo);


                //UNISTENJE
                Destroy(hitObject);
                hitObject = null;
                item = null;

                Debug.Log("Svarozic skupljen");
            }
            break;

            case "Malik":
            tooltips.text = "(" + pickUpKey.ToString() + ") Pokupi Malika Tintilinića";
            if (skupi) {
                svarozicSkripta.enabled = true;
                svarozicURuci.SetActive(true);

                //promijeni audiosource da se stvar ne zbrejka

                subtitlesSkripta.malikSourcePocetak = subtitlesSkripta.malikSource;
                itemSound.PlayOneShot(pickUo);


                //UNISTENJE
                Destroy(hitObject);
                hitObject = null;
                item = null;

                
            }
            break;

            case "VrataKamenolom":
            tooltips.text = "(" + pickUpKey.ToString() + ") Otvori vrata kamenoloma";
            if (skupi && ProstorEnums.striborProgress == ProstorEnums.StriborProgression.SkupioKljuc) {
                

                //otvori vrata kamenoloma
                vrata.SetBool("vrata", true);
                itemSound.PlayOneShot(pickUo);
                
        
            }
            break;

            case "Jelen":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi jelena";
            if (skupi && ProstorEnums.striborProgress == ProstorEnums.StriborProgression.SkupioKljuc) {
                hitObject.SetActive(false);

                ProstorEnums.striborProgress = ProstorEnums.StriborProgression.SkupioJelena;
                titlovi.ukljuciTitlove("SkupioJelena");
                itemSound.PlayOneShot(pickUo);
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
            tooltips.text = "(" + pickUpKey.ToString() + ") Skupi ključ od kamenoloma";
            if (skupi) {
                hitObject.SetActive(false);
                ProstorEnums.striborProgress = ProstorEnums.StriborProgression.SkupioKljuc;
                titlovi.ukljuciTitlove("SkupioKljuc");
                itemSound.PlayOneShot(pickUo);
            }
            break;

            case "Kotao":
            tooltips.text = "(" + pickUpKey.ToString() + ") Skuhaj jelenice";
            if (skupi && brojJelenica == 12) {
                //endaj game
                Debug.Log("Gotova igra");
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
                
            }
            tooltips.enabled = false;
            
        }



    }

    

}
