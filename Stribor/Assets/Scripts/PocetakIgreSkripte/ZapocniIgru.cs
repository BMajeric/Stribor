using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapocniIgru : MonoBehaviour
{
    public List<Vector3> pozicijeJelenica = new List<Vector3>(); //pozicije na kojima ce se randomly rasporedit sve jelenice

    public List<Vector3> tamnaSumaPozicije = new List<Vector3>(); //2 jelenice sigurno idu u tamnu sumu

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

    // Start is called before the first frame update
    void Start()
    {
        triger2.SetActive(false);
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
            int random = Random.Range(0, tamnaSumaPozicije.Count);

            while (iskoristeniT.Contains(random)) {
                random = Random.Range(0, tamnaSumaPozicije.Count);
            }
            iskoristeniT.Add(random);

            jelenica.localPosition = tamnaSumaPozicije[random];
        } else {
            //strpaj u ostatak svijeta
            int random = Random.Range(0, pozicijeJelenica.Count);
            while (iskoristeni.Contains(random)) {
                random = Random.Range(0, tamnaSumaPozicije.Count);
            }
            iskoristeni.Add(random);
            jelenica.localPosition = pozicijeJelenica[random];
        }

        brojac += 1;

    }

   }
}
