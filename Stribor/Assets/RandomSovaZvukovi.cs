using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSovaZvukovi : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource auSrc;
    public AudioClip sova;
    void Start()
    {
        auSrc = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Update is called once per frame
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(10f, 60f));

        auSrc.PlayOneShot(sova, 0.4f);

        yield return new WaitForSeconds(sova.length);
        StartCoroutine(PlaySound());
    }
}
