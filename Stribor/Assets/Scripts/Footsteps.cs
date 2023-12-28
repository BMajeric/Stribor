using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    FirstPersonController playerSkripta;

    private AudioSource audioSource;

    public float timeBetweenSteps;

    float vrijemeOdKoraka;

    float omjerTrcanja; //brzina hodanja / brzina sprintanja

    public AudioClip[] footstepSounds; //lista zvukova
    // Start is called before the first frame update
    void Start()
    {
        playerSkripta = this.GetComponent<FirstPersonController>();
        audioSource = this.GetComponent<AudioSource>();
        vrijemeOdKoraka = Time.time;

        omjerTrcanja = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSkripta.isSprinting) {
            omjerTrcanja = playerSkripta.walkSpeed / playerSkripta.sprintSpeed;
        } else {
            omjerTrcanja = 1f;
        }

        if (playerSkripta.isWalking && Time.time - vrijemeOdKoraka >= timeBetweenSteps * omjerTrcanja) {

            AudioClip randomZvuk = footstepSounds[Random.Range(0, footstepSounds.Length)];

            audioSource.PlayOneShot(randomZvuk);

            vrijemeOdKoraka = Time.time;

        }
        
    }
}
