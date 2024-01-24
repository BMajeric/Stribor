using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapocniIgru : MonoBehaviour
{
    public List<Vector3> pozicijeJelenica = new List<Vector3>(); //pozicije na kojima ce se randomly rasporedit sve jelenice

    public List<Vector3> tamnaSumaPozicije = new List<Vector3>(); //2 jelenice sigurno idu u tamnu sumu

    public List<Vector3> pozicijeJelenicaCopy; //pozicije na kojima ce se randomly rasporedit sve jelenice

    public List<Vector3> tamnaSumaPozicijeCopy;

    public List<Transform> jelenice;

    List<int> iskoristeniT = new List<int>();

    List<int> iskoristeni = new List<int>();

    public GameObject triger2;

    VrijemeSkripta vrijeme;

    public GameObject player;

    public GameObject zoom;

    RaycastingForItems raycasting;

    JelenicaRadar radar;

    SvarozicGaming svarozicGaming;

    Titlovi titlovi;

    public GameObject triger1;

    public GameObject svarozicURuci;

    int random;

    // Start is called before the first frame update
    void Start()
    {
        triger2.SetActive(false);
        foreach (Vector3 pozicija in tamnaSumaPozicije) {
            tamnaSumaPozicijeCopy.Add(pozicija);
        }

        foreach (Vector3 pozicija in pozicijeJelenica) {
            pozicijeJelenicaCopy.Add(pozicija);
        }
        
        rasporediJelenice();

        vrijeme = GameObject.FindGameObjectWithTag("Sunce").GetComponent<VrijemeSkripta>();

        ProstorEnums.smrtIgraca = ProstorEnums.Smrt.UmroJednom;

        vrijeme.PromijeniVrijeme(2);

        raycasting = player.GetComponent<RaycastingForItems>();
        svarozicGaming = player.GetComponent<SvarozicGaming>();
        radar = zoom.GetComponent<JelenicaRadar>();
        
        titlovi = GameObject.FindGameObjectWithTag("Titlovi").GetComponent<Titlovi>();

        //za brzo pocinjanje
        radar.enabled = true;
        raycasting.enabled = true;
        svarozicGaming.enabled = true;
        titlovi.mozeTitlovati = true;
        titlovi.pocetak = false;
        triger1.SetActive(false);
        svarozicURuci.SetActive(true);

        
    }

   void rasporediJelenice() {
    //randomly postavi jelenice na pozicije

    int brojac = 0;

    foreach (Transform jelenica in jelenice) {

        jelenica.gameObject.SetActive(true);

        if (brojac < 2) {
            // strpaj u tamnu sumu
            random = Random.Range(0, tamnaSumaPozicijeCopy.Count);

            jelenica.localPosition = tamnaSumaPozicijeCopy[random];

            tamnaSumaPozicijeCopy.RemoveAt(random);

        } else {
            //strpaj u ostatak svijeta
            random = Random.Range(0, pozicijeJelenicaCopy.Count);
            jelenica.localPosition = pozicijeJelenicaCopy[random];
            pozicijeJelenicaCopy.RemoveAt(random);
            Debug.Log(pozicijeJelenicaCopy.Count);
            


        }

        brojac += 1;

    }

   }

   public void RasporediJeleniceNakonSmrti() {
    
    int brojacJ = 0;

    List<Transform> iskljuceneJelenice = new List<Transform>();

    foreach (Transform jelenica in jelenice) {

        if (!jelenica.gameObject.activeInHierarchy && brojacJ < 2) {

            brojacJ += 1;

            iskljuceneJelenice.Add(jelenica);
        }

    }

    foreach (Transform jelenica in iskljuceneJelenice) {
        jelenica.gameObject.SetActive(true);

        random = Random.Range(0, pozicijeJelenicaCopy.Count);
        jelenica.localPosition = pozicijeJelenicaCopy[random];
        pozicijeJelenicaCopy.RemoveAt(random);
    }
    

   }
}
