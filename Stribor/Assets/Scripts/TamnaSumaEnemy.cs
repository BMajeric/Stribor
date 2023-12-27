using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TamnaSumaEnemy : MonoBehaviour
{
    GameObject player;

    public GameObject TamniEnemy;

    public float brojac;

    public List<float> SpawnCooldown; //koliko cesto izmedu napada
    public List<float> SpawnFrequency; //random period kada moze napasti

    

    public List<Vector3> Pozicije; //pozicije u tamnoj sumi na kojima se moze spawnat

    public float trenutniCooldown;

    public float trenutniFrequency;

    TamnaSumaArea tamnaSuma;
    public float startTime;

    public float currentTime;

    bool onCooldown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //startTime = Time.time;

        DefineCooldownAndFrequency();

        tamnaSuma = this.GetComponent<TamnaSumaArea>();

        

    }

    void OnEnable() {
        startTime = Time.time;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            SpawnEnemy();
        }

        if (tamnaSuma.vani) {
            currentTime = Time.time - startTime;
            if (currentTime > trenutniFrequency && !onCooldown) {
                startTime = Time.time;
                onCooldown = true;
                SpawnEnemy();

            } else if (TamniEnemy.activeSelf) {
                startTime = Time.time;
            } else if (onCooldown && currentTime > trenutniCooldown && !TamniEnemy.activeSelf) {
                startTime = Time.time;
                onCooldown = false;
                DefineCooldownAndFrequency();
            }

        }

        

    }

    void DefineCooldownAndFrequency() {

        trenutniCooldown = UnityEngine.Random.Range(SpawnCooldown[0], SpawnCooldown[1]);

        trenutniFrequency = UnityEngine.Random.Range(SpawnFrequency[0], SpawnFrequency[1]);

        onCooldown = false;

    }
    

    void SpawnEnemy() {

        Debug.Log("Spawnao");

        //Pronadi najblizu poziciju i nemoj ju koristit

        //wpawnaj ga randomly na nekoj drugoj poziciji

        int najmanjiIndex = 0;

        foreach (Vector3 pozicija in Pozicije) {

            float distance = Vector3.Distance(pozicija, player.transform.position);

            if (distance < Vector3.Distance(Pozicije[najmanjiIndex], player.transform.position)) {
                najmanjiIndex = Pozicije.IndexOf(pozicija);
            }



        }

        int randomBroj = najmanjiIndex;

        while (randomBroj == najmanjiIndex) {
            randomBroj = UnityEngine.Random.Range(0, Pozicije.Count - 1);
        }

        TamniEnemy.transform.position = Pozicije[randomBroj];

        TamniEnemy.SetActive(true);

        



    }
}
