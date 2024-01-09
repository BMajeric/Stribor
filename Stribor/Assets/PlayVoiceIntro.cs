using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayVoiceIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip engineStartClip;
    public AudioClip engineLoopClip;
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.loop = false;
        StartCoroutine(playEngineSound());
    }

    IEnumerator playEngineSound()
    {
        audio.clip = engineStartClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = engineLoopClip;
        audio.Play();
    }
}
