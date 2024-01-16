using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class JelenicaRadar : MonoBehaviour
{
    ParticleSystem radar;
    float cooldownTime = 5.0f;
    float lastUsed;

    public KeyCode radarKey;

    UniversalRendererData universalRendererData;

    public ScriptableRendererFeature jelenicaOccluded;

    AudioSource zvukRadara;

    // Start is called before the first frame update
    void Start()
    {
        radar = GetComponent<ParticleSystem>();
        zvukRadara = GetComponent<AudioSource>();
        radarKey = KeyCode.G;
    }

    // Update is called once per frame
    private void Update() {
        
        if (Input.GetKeyDown(radarKey) && Time.time > lastUsed + cooldownTime) {
            radar.Play();

            //zvukRadara.Play();

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
