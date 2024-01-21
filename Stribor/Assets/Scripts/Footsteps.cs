using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    FirstPersonController playerSkripta;

    private AudioSource audioSource;

    public float timeBetweenSteps;

    float vrijemeOdKoraka;

    public Transform ociLevel;

    float omjerTrcanja; //brzina hodanja / brzina sprintanja

    public AudioClip[] footstepSoundsTerrain; //lista zvukova trave

    public AudioClip[] footstepSoundsIndoors;

    public AudioClip[] footstepSoundsSpilja;

    RaycastHit hitGround;

    LayerMask terrainLayer;

    private float lowPitch = 0.75f;

    private float highPitch = 1.25f;

    string pod; //terrain, unutra ili spilja
    // Start is called before the first frame update
    void Start()
    {
        playerSkripta = this.GetComponent<FirstPersonController>();
        audioSource = this.GetComponent<AudioSource>();
        vrijemeOdKoraka = Time.time;

        omjerTrcanja = 1f;

        terrainLayer = LayerMask.GetMask("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSkripta.isSprinting) {
            omjerTrcanja = playerSkripta.walkSpeed / playerSkripta.sprintSpeed;
        } else {
            omjerTrcanja = 1f;
        }

        //raycast za pod
        if (Physics.Raycast(ociLevel.position, Vector3.down, out hitGround, 3)) {
            
            pod = hitGround.transform.tag;

        } else {
            pod = "";
        }

        if (playerSkripta.isWalking && Time.time - vrijemeOdKoraka >= timeBetweenSteps * omjerTrcanja) {
            AudioClip randomZvuk;
            switch(pod) {

                case "Terrain":
                randomZvuk = footstepSoundsTerrain[Random.Range(0, footstepSoundsTerrain.Length)];
                break;

                case "Indoors":
                randomZvuk = footstepSoundsIndoors[Random.Range(0, footstepSoundsIndoors.Length)];
                break;

                case "Spilja":
                randomZvuk = footstepSoundsSpilja[Random.Range(0, footstepSoundsSpilja.Length)];
                break;

                default:
                randomZvuk = null;
                break;

            }
            
            float randomPitch = Random.Range(lowPitch, highPitch);
            audioSource.pitch = randomPitch;
            if (randomZvuk != null) {
                audioSource.PlayOneShot(randomZvuk);
            }
           

            vrijemeOdKoraka = Time.time;

        }
        
    }
}
