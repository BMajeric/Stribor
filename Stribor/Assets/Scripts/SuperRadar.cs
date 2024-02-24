using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SuperRadar : MonoBehaviour
{
    public AudioSource audioSource;

    public ParticleSystem superRadar;

    public ScriptableRendererFeature jelenicaOccluded;

    RaycastingForItems raycasting;

    public List<GameObject> jeleniceLista = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        raycasting = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>();
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
        Debug.Log("Palim velike jelenice");

        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

        ParticleSystem.ColliderData colliderData;

        superRadar.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles, out colliderData);

        //iteriraj po svima i ukljuci ih i cini ih velikima
        foreach (GameObject jelenica in jeleniceLista) {

            jelenica.transform.Find("BigJelenica").gameObject.SetActive(true);
        }

        
        //PogodenaJelenica.transform.Find("Particle").GetComponent<ParticleSystem>().Play();

        //ukljuci rendere occlusion da se jelenica i efekti vide kroz zidove

        jelenicaOccluded.SetActive(true);

        StartCoroutine(GasiOcclusionIVelikeJelenice(15.0f));

        

    }

    IEnumerator GasiOcclusionIVelikeJelenice(float vrijeme) {

        float vrijemeCekanja = 0f;

        while (vrijemeCekanja < vrijeme) {

            yield return new WaitForSeconds(1.0f);
            vrijemeCekanja += 1f;

            if (!raycasting.naTornju) {
                jelenicaOccluded.SetActive(false);

                foreach (GameObject jelenica in jeleniceLista) {

                    jelenica.transform.Find("BigJelenica").gameObject.SetActive(false);
                }

                yield break;
            }
        }

        

        jelenicaOccluded.SetActive(false);

        foreach (GameObject jelenica in jeleniceLista) {

            jelenica.transform.Find("BigJelenica").gameObject.SetActive(false);
        }

    }


}
