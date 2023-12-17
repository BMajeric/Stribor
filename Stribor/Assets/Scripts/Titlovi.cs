using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlovi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Subtitles.Show("Jebem titlove", 4f, SubtitleEffect.Fade, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
