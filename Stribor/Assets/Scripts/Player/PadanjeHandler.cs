using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadanjeHandler : MonoBehaviour
{
    //Skripta za handleanje smrti tijekom velikog pada i slamanja nogu i tako

    FirstPersonController playerController;

    public float visinaPrijePada;

    public float visinaNakonPada;

    public float damageThreshold; //koliko igrac treba pasti da si slomi noge

    public float speedLimit; //kolko igracu treba slomit noge

    public float speedLimitDuration; //koliko dugo ima slomljene noge

    PlayerDeath death;

    bool inAir;

    bool grounded;

    public AudioSource audioSource;

    public AudioClip lamanjeNogu;

    public bool hitDeathPlane; //neki veliki dropovi triggeraju smrt

    void Start()
    {
        inAir = false;
        grounded = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        visinaPrijePada = playerController.transform.localPosition.y;
        visinaNakonPada = playerController.transform.localPosition.y;
        death = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //provjeri stanje igraca
        if (playerController.isGrounded && !grounded) {
            //igrac se vratio na pod, usporedi vrijednosti
            Debug.Log("Landed");
            grounded = true;
            visinaNakonPada = playerController.transform.localPosition.y;

            if (Math.Abs(visinaPrijePada - visinaNakonPada) > damageThreshold) {
                //polomi igracu noge
                playerController.walkSpeed *= speedLimit;
                playerController.sprintSpeed *= speedLimit;

                audioSource.PlayOneShot(lamanjeNogu);

                StartCoroutine(oporaviIgraca(speedLimitDuration));
            }

            if (hitDeathPlane) {
                hitDeathPlane = false;
                death.UbijIgraca();
            }

        } else if (!playerController.isGrounded && grounded) {
            //player je skocio ili pada, zapamti y vrijednost
            Debug.Log("Left Ground");
            visinaPrijePada = playerController.transform.localPosition.y;
            grounded = false;
        }

        
    }

    IEnumerator oporaviIgraca(float vrijeme) {

        yield return new WaitForSeconds(vrijeme);

        playerController.walkSpeed /= speedLimit;
        playerController.sprintSpeed /= speedLimit;

    }
}
