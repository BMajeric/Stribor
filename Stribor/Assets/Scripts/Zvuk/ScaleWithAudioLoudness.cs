using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithAudioLoudness : MonoBehaviour
{
    public Vector3 minScale, maxScale;
    public MicrophonePickup dectector;

    public float Sensiblity = 200.0f;
    public float treshold = 1.0f;
    private float loudness = 0.0f;

    private void Update()
    {
        loudness = dectector.getLoudnessFromMic() * Sensiblity;
        //Debug.Log(loudness);
        if(loudness < treshold ) loudness = 0;

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.transform.position, loudness);
    }
}
