using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;
using System;

public class PlayerDeath : MonoBehaviour
{
    
    public Collider deathCollider;

    public Animator animator;

    FirstPersonController playerSkripta;

    public float trast;

    public float brzinaRasta;

    public Transform respawnPos;

    RaycastingForItems raycastSkripta;

    public ZapocniIgru zapocniIgruSkripta;

    public AudioSource audioSource;

    public bool umire;

    public GameObject enemy;

    private void Start() {
        playerSkripta = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        raycastSkripta = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>();
        umire = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            UbijIgraca();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !umire) {
            UbijIgraca();
            
        } else if (collision.gameObject.tag == "Enemy") {
            float randomPitch = UnityEngine.Random.Range(0.75f, 1.25f);
            audioSource.pitch = randomPitch;
            audioSource.Play();
        }
        
    }

    IEnumerator RespawnajGuju() {
        //nakon smrti despawanj ju i respawnaj za minutu
        enemy.SetActive(false);

        yield return new WaitForSeconds(60f);

        enemy.SetActive(true);
    }

    public void UbijIgraca() {
        umire = true;
        //iskljuci movement
        playerSkripta.playerCanMove[2] = false;
        playerSkripta.cameraCanMove = false;
        playerSkripta.isWalking = false;
        //Debug.Log(playerSkripta.transform.rotation.y);
        audioSource.pitch = 1f;
        audioSource.Play();

        StartCoroutine(OkreciIgraca(0f));


    }

    IEnumerator OkreciIgraca(float t) {
        t += trast;

        Vector3 lerpRotation = Vector3.Lerp(new Vector3(0f, playerSkripta.transform.localEulerAngles.y, 0f), new Vector3(-90f, playerSkripta.transform.localEulerAngles.y, 0f), t);

        playerSkripta.transform.rotation = Quaternion.Euler(lerpRotation.x, lerpRotation.y, lerpRotation.z);

        yield return new WaitForSeconds(brzinaRasta);

        if (t < 1f) {
            StartCoroutine(OkreciIgraca(t));
        } else {
            StartCoroutine(srediSmrt());

            //Zatamni skrin i respawnaj igraca
        }
    }

    IEnumerator srediSmrt() {

        Debug.Log("Umro skroz");
        animator.Play("Base Layer.GameFadeIn");
        yield return new WaitForSeconds(8.5f);

        StartCoroutine(RespawnajGuju());

        //minusaj jelenice

        if (raycastSkripta.brojJelenica >= 2) {
            raycastSkripta.brojJelenica -= 2;
                
        } else {
            raycastSkripta.brojJelenica = 0;
        }

        raycastSkripta.JeleniceTekst.text = "Jelenice: " + raycastSkripta.brojJelenica;

        //respawnaj te dvije jelenice

        if (ProstorEnums.smrtIgraca != ProstorEnums.Smrt.NijeUmro) {
            zapocniIgruSkripta.RasporediJeleniceNakonSmrti();
        } else {
            zapocniIgruSkripta.enabled = true;
        }

        

        //postavi smrt enum

        switch (ProstorEnums.smrtIgraca) {

            case ProstorEnums.Smrt.NijeUmro:
            ProstorEnums.smrtIgraca = ProstorEnums.Smrt.UmroJednom;
            StartCoroutine(respawnajIgraca());
            break;

            case ProstorEnums.Smrt.UmroJednom:
            ProstorEnums.smrtIgraca = ProstorEnums.Smrt.UmroDvaput;
            StartCoroutine(respawnajIgraca());
            break;

            case ProstorEnums.Smrt.UmroDvaput:
            ProstorEnums.smrtIgraca = ProstorEnums.Smrt.UmroTriput;
            StartCoroutine(respawnajIgraca());
            break;

            case ProstorEnums.Smrt.UmroTriput:
            //neki skrin il nesto
            Debug.Log("Toliko je gotovo");
            break;

        }
    }

    IEnumerator respawnajIgraca() {

        animator.Play("Base Layer.GameFadeOut");
        playerSkripta.transform.position = respawnPos.position;

        playerSkripta.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        yield return new WaitForSeconds(5f);

        playerSkripta.playerCanMove[2] = true;
        playerSkripta.cameraCanMove = true;
        umire = false;

        

        

    }
}
