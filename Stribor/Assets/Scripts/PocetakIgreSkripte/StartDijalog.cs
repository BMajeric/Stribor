using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDijalog : MonoBehaviour
{
    // Start is called before the first frame update

    int brojac = 0;

    bool trigerao = false;

    public GameObject start;



    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            start.GetComponent<ZapocniIgru>().enabled = true;
        }
    }
   
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player" && !trigerao) {
        trigerao = true;
        StartCoroutine(iritirajIgraca());
    }

   }


   IEnumerator iritirajIgraca() {

    if (brojac == 0) {
        Subtitles.Show("Hej stranče, izađi iz čarobne šume i dođi do mene.", 8f, SubtitleEffect.Both, 25);
    } else if (brojac == 5) {
        Subtitles.Show("Hej budalo gdje si otišao, nema ništa tamo.", 8f, SubtitleEffect.Both, 25);
    } else {
        Subtitles.Show("Ovdje sam na poljani.", 8f, SubtitleEffect.Both, 25);

    }

    brojac += 1;
    yield return new WaitForSeconds(15f);

    StartCoroutine(iritirajIgraca());

   }
}
