using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophonePickup : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphone;
    private string micname;

    private void MicrophoneAudioClip(int micIndex)
    {
        float loudestMic = 0;
        float levelMax = 0;
        string maxMic = "";
        float[] waveData = new float[128];
        foreach (var ind in Microphone.devices)
        {
            int micPos = Microphone.GetPosition(ind);
            try { microphone.GetData(waveData, micPos); }
            catch { }

            for (int i = 0; i < 128; i++) {
                float peak = waveData[i] * waveData[i];
                if (peak > levelMax)
                {
                    levelMax = peak;
                }
            }

            if(levelMax > loudestMic)
            {
                levelMax = loudestMic;
                maxMic = ind;
            }
        }

        if(maxMic == "")
        {
            micname = Microphone.devices[0];
        } else
        {
            int pos = Microphone.GetPosition(maxMic);
            micname = Microphone.devices[pos];
        }
;
        microphone = Microphone.Start(micname, true, 20, AudioSettings.outputSampleRate);
    } 

    public float getLoudnessFromMic()
    {
        return GetLoudnessAudioClip(Microphone.GetPosition(micname), microphone);
    }

    private void Start()
    {
        MicrophoneAudioClip(0);
    }

    public float GetLoudnessAudioClip(int clipPos, AudioClip clip)
    {
        int startPos = clipPos - sampleWindow;

        if (startPos < 0) { return 0;}

        float[] wave = new float[sampleWindow];
        clip.GetData(wave, startPos);

        float loudness = 0;

        foreach (int i in wave)
        {
            loudness += Mathf.Abs(i);
        }

        return loudness / sampleWindow;
    }
}
