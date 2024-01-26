using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.Universal;

public class JelenicaRadar : MonoBehaviour
{
    ParticleSystem radar;
    float cooldownTime = 8.0f;
    float lastUsed;

    public KeyCode radarKey;

    UniversalRendererData universalRendererData;

    public ScriptableRendererFeature jelenicaOccluded;

    public AudioSource zvukRadara;

    public List<AudioClip> radarZvukovi = new List<AudioClip>();

    ExposureManager exposureManager;

    // Start is called before the first frame update
    void Start()
    {
        radar = GetComponent<ParticleSystem>();
        
        radarKey = KeyCode.G;

        exposureManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ExposureManager>();
    }

    // Update is called once per frame
    private void Update() {
        
        if (Input.GetKeyDown(radarKey) && Time.time > lastUsed + cooldownTime) {

            AudioClip randomClip = radarZvukovi[Random.Range(0, radarZvukovi.Count)];

            zvukRadara.PlayOneShot(randomClip);

            radar.Play();

            //zvukRadara.Play();

            exposureManager.Exposure += 10;

            lastUsed = Time.time;
        }


    }

    private void OnParticleTrigger() {
        Debug.Log("Pogodio jelenicu");

        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

        ParticleSystem.ColliderData colliderData;

        radar.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles, out colliderData);

        GameObject PogodenaJelenica = colliderData.GetCollider(0, 0).gameObject;

        PogodenaJelenica.GetComponent<AudioSource>().Play();

        //playay particle sistem jelenice

        PogodenaJelenica.GetComponent<ParticleSystem>().Play();
        //PogodenaJelenica.transform.Find("Particle").GetComponent<ParticleSystem>().Play();

        //ukljuci rendere occlusion da se jelenica i efekti vide kroz zidove

        jelenicaOccluded.SetActive(true);

        StartCoroutine(GasiOcclusion());

    }

    IEnumerator GasiOcclusion() {

        yield return new WaitForSeconds(5.0f);

        jelenicaOccluded.SetActive(false);

    }

}
