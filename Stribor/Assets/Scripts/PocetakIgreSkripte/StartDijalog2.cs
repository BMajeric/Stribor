using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDijalog2 : MonoBehaviour
{
    // Start is called before the first frame update

    int brojac = 0;

    public GameObject triger1;

    public GameObject player;

    public GameObject zoom;

    RaycastingForItems raycasting;

    JelenicaRadar radar;

    SvarozicGaming svarozicGaming;

    Titlovi titlovi;

    bool trigerao;

    public GameObject enemy;

    PlayerDeath death;

    private void Start() {
        raycasting = player.GetComponent<RaycastingForItems>();
        svarozicGaming = player.GetComponent<SvarozicGaming>();
        death = player.GetComponent<PlayerDeath>();
        radar = zoom.GetComponent<JelenicaRadar>();
        
        titlovi = GameObject.FindGameObjectWithTag("Titlovi").GetComponent<Titlovi>();
        trigerao = false;
    }
    
   
    private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player" && !trigerao) {
        trigerao = true;
        triger1.SetActive(false);
        
        Subtitles.Show("Dobrodošao u čarobnu šumu, ja sam Malik Tintilinić.", 8f, SubtitleEffect.Both, 25);
        StartCoroutine(Dijalog());
    }

   }

   IEnumerator Dijalog() {

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Kao što vidiš, nad šumom je neprirodna tama od onoga vremena kada je sumorna guja bacila svoju zlu čaroliju", 8f, SubtitleEffect.Both, 25);

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Starješina Stribor nije svjestan gujinog utjecaja i ti nam trebaš pomoći.", 8f, SubtitleEffect.Both, 25);

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Čarobna šuma sadrži sve što nam treba za lijek, pronađi 12 jelenica i baci ih u ovaj kotao.", 8f, SubtitleEffect.Both, 25);

    radar.enabled = true;

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Jednu sam već uhvatio ovdje, vidiš. Ostale ćeš moći uhvatiti ovom zviždaljkom. (" + radar.radarKey.ToString() + " na tipkovnici)", 8f, SubtitleEffect.Both, 25);

    raycasting.enabled = true;
    yield return new WaitUntil(() => Input.GetKeyDown(radar.radarKey));

    

    Subtitles.Show("Odlično zviždanje, vidiš li kako se jelenica oglasila, sada ju zgrabi što brže!", 8f, SubtitleEffect.Both, 25);

    yield return new WaitUntil(() => raycasting.brojJelenica == 1);

    Subtitles.Show("Tako treba, još samo 11 i izliječiti ćemo šumu. Pokupi sada i mene, imam mnogo mudrosti koje ti mogu šaptati u uho tijekom avanture.", 8f, SubtitleEffect.Both, 25);

    yield return new WaitUntil(() => svarozicGaming.enabled);

    Subtitles.Show("Odlično, moje svijetlo će osvijetliti tamne dijelove šume. Ako pronađeš još Domaćih, skupi ih. Ako ti trebaju mudrosti, samo reci (" + titlovi.hint.ToString() +  " na tipkovnici)", 8f, SubtitleEffect.Both, 25);

    titlovi.mozeTitlovati = true;

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Ako smo u opasnosti, samo javi i sakrit ću ti se u kaputu (" + svarozicGaming.LightGumb.ToString()  + " na tipkovnici)", 8f, SubtitleEffect.Both, 25);

    yield return new WaitForSeconds(12f);

    Subtitles.Show("Mislim da sam čuo jelenicu negdje na onom drveću, idemo ju skupiti. Pazi se! Zla guja je još uvijek u blizini.", 8f, SubtitleEffect.Both, 25);

    yield return new WaitUntil(() => raycasting.brojJelenica == 2);

    enemy.SetActive(true);

    StartCoroutine(backupSmrt());



   }

   IEnumerator backupSmrt() {
    //ubi igraca ako ga ne ubije guja

    yield return new WaitForSeconds(60f);

    if (ProstorEnums.smrtIgraca == ProstorEnums.Smrt.NijeUmro) {
        death.UbijIgraca();
    }

   }
}
