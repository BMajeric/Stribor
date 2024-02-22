using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRadar : MonoBehaviour
{
    public AudioSource audioSource;

    public ParticleSystem superRadar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SuperRadarPali() {
        //SUPER RADARRRR!!!!!!
        audioSource.Play();

        superRadar.Play();



    }

    private void OnParticleTrigger() {
        Debug.Log("Pogodio jelenicu");

        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

        ParticleSystem.ColliderData colliderData;

        superRadar.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles, out colliderData);

        GameObject PogodenaJelenica = colliderData.GetCollider(0, 0).gameObject;

        PogodenaJelenica.GetComponent<AudioSource>().Play();

        //playay particle sistem jelenice

        PogodenaJelenica.GetComponent<ParticleSystem>().Play();
        //PogodenaJelenica.transform.Find("Particle").GetComponent<ParticleSystem>().Play();

        //ukljuci rendere occlusion da se jelenica i efekti vide kroz zidove

        

        

    }

}
