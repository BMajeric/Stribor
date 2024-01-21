using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        yield return new WaitForSeconds(0.2f);
        audio.clip = engineStartClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = engineLoopClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        SceneManager.LoadScene("SampleScene");
    }
}
