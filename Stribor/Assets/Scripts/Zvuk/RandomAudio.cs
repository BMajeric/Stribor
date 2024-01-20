using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudio : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioClip[] clips;
    private int clipIndex;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Update is called once per frame
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));

        clipIndex = Random.Range(0, clips.Length - 1);
        audio.PlayOneShot(clips[clipIndex], 0.15f);

        yield return new WaitForSeconds(clips[clipIndex].length);
        StartCoroutine(PlaySound());
    }
}
