using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamnaSumaEnemySpawned : MonoBehaviour
{
    GameObject player;

    FirstPersonController playerSkripta;

    public GameObject tamnaSuma;

    TamnaSumaArea tamnaSumaSkripta;

    SvarozicGaming svarozicSkripta;

    public GameObject umrositekst;

    float startTime;

    public float step;

    public float distanceZaNapad; //kada ce napasti igraca

    bool uNapadu;

    public AudioSource napadZvuk;

    public AudioSource lisceZvuk;

    float vrijemeAktivno;

    bool bijeg; //za trcanje od igraca i despawnanje

    float distance; //udaljenost izmedu enemya i igraca
    // Start is called before the first frame update

    RaycastHit hitGround;

    LayerMask terrainMask;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerSkripta = player.GetComponent<FirstPersonController>();

        svarozicSkripta = player.GetComponent<SvarozicGaming>();

        startTime = Time.time;

        tamnaSumaSkripta = tamnaSuma.GetComponent<TamnaSumaArea>();

        Debug.Log("start");

        terrainMask = LayerMask.GetMask("Terrain");

        

    }

    void OnEnable()
    {
        uNapadu = false;

        bijeg = false;

        vrijemeAktivno = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(player.GetComponent<Rigidbody>().velocity.magnitude);
        //napravi da je uvijek okrenut prema igracu

        this.transform.LookAt(player.transform, Vector3.up);
        this.transform.Rotate(0f, -90f, 0f);

        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance > distanceZaNapad && tamnaSumaSkripta.vani && !bijeg) {
            KreciSePremaIgracu();
        } else if (distance <= distanceZaNapad && !uNapadu) {
            uNapadu = true;
            StartCoroutine(napadniIgraca());
        } else if (bijeg) {
            KreciSeOdIgraca();
        }

        if (Time.time - vrijemeAktivno > 120f && Time.time - vrijemeAktivno < 123f) {
            bijeg = true;
        } else if (Time.time - vrijemeAktivno > 123f) {
            this.gameObject.SetActive(false);
        }

        //napravi da je y vrijednost uvijek oko zemlje da ne clipa kroz shit

        //raycast dolje
        //Debug.DrawRay(this.transform.position, Vector3.down, Color.red, 2);
        if (Physics.Raycast(this.transform.position, Vector3.down, out hitGround, 20, terrainMask)) {
            
            //this.transform.position = new Vector3(this.transform.position.x, hitGround.transform.position.y, this. transform.position.z);
            this.transform.position = hitGround.point + new Vector3(0f, this.transform.localScale.y, 0f);
            
        } else if (Physics.Raycast(this.transform.position, Vector3.up, out hitGround, 20, terrainMask)) {
            //raycast gore
            
            //this.transform.position = new Vector3(this.transform.position.x, hitGround.transform.position.y, this. transform.position.z);
            this.transform.position = hitGround.point + new Vector3(0f, this.transform.localScale.y, 0f);
        }
        
        
    }

    void KreciSePremaIgracu() {

        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, step * Time.deltaTime);

    }

    void KreciSeOdIgraca() {

        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, -step * Time.deltaTime * 10);

    }

    IEnumerator napadniIgraca() {
        Debug.Log("Napad");

        napadZvuk.Play();

        lisceZvuk.Pause();

        //kada je dovoljno blizu pocet ce rezat i igrac mora imat ugaseno svijetlo i ne kretat se, ako ne uspije ce ga ubiti
        yield return new WaitForSeconds(2.5f); //pocetni dio rezanja

        //provjera za ubojstvo

        while (napadZvuk.isPlaying) {
            //provjeravaj za kretanje svaki frame

            if (player.GetComponent<Rigidbody>().velocity.magnitude > 0.5f || !svarozicSkripta.SvarozicUgasen) {
                ubijIgraca();
                yield break;
            }

            


            yield return new WaitForEndOfFrame();

        }
        //ako napad nije uspio
        Debug.Log("Prezivio");
        lisceZvuk.UnPause();
        bijeg = true;
        yield return new WaitForSeconds(3f);
        
        this.gameObject.SetActive(false);
        
    }

    void ubijIgraca() {
        //neki jumpscare ili nesto
        umrositekst.SetActive(true);
        playerSkripta.playerCanMove[2] = false;
        playerSkripta.cameraCanMove = false;
        Camera.main.transform.LookAt(this.transform);
    }
}
