using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriborSpiljaSkripta : MonoBehaviour
{
    public GameObject SpiljaBlokada;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && ProstorEnums.striborProgress < ProstorEnums.StriborProgression.SkupioJelena) {
            //igrac ne smije u spilju
            Subtitles.Show("Ne možemo pred starješinu bez jelena! Mislim da sam čuo jednog u kamenolomu.", 5f, SubtitleEffect.Both, 25);

        } else if (other.tag == "Player" && ProstorEnums.striborProgress >= ProstorEnums.StriborProgression.SkupioJelena) {
            //makni collidere i dozvoli ulazak u spilju
            Subtitles.Show("Sa jelenom zajašimo do Stribora dublje u špilju, hajdemo!", 5f, SubtitleEffect.Both, 25);
            SpiljaBlokada.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
