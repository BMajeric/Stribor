using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KotaoSkripta : MonoBehaviour
{
    public GameObject jelenicaCanvas;

    public TMP_Text jelenicaUKotlu;

    RaycastingForItems raycasting;

    public int brojSkuhanihJelenica;
    // Start is called before the first frame update
    void Start()
    {
        raycasting = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //neku animaciju kasnije
    public void prikaziTekst() {
        if (jelenicaCanvas.activeSelf == false) {
            jelenicaCanvas.SetActive(true);
            StartCoroutine(sakrijTekst());

        }
        
    }

    IEnumerator sakrijTekst() {
        yield return new WaitForSeconds(5.0f);
        jelenicaCanvas.SetActive(false);
    }

    public void skuhajJelenice(int brojZaSkuhat) {

        brojSkuhanihJelenica += brojZaSkuhat;

        jelenicaUKotlu.text = brojSkuhanihJelenica.ToString() + "/12";

    }

    public void krajIgre() {
        Debug.Log("Gotova igra");
        SceneManager.LoadScene("Ending");
    }
}
