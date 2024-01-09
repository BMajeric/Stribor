using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithAudioLoudness : MonoBehaviour
{
    public Vector3 minScale, maxScale;
    public MicrophonePickup dectector;

    public float Sensiblity = 100.0f;
    public float treshold = 1.0f;

    private void Update()
    {
        float loudness = dectector.getLoudnessFromMic() * Sensiblity;
        if(loudness < treshold ) loudness = 0;

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
