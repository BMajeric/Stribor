using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSpatialAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private int clipIndex;
    AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Update is called once per frame
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));

        clipIndex = Random.Range(0, clips.Length - 1);
        audioSrc.spatialBlend = 1.0f;
        audioSrc.PlayOneShot(clips[clipIndex], 0.8f);

        yield return new WaitForSeconds(clips[clipIndex].length);
        StartCoroutine(PlaySound());
    }
}