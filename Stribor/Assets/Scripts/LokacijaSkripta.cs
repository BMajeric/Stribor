using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class LokacijaSkripta : MonoBehaviour
{
    //Skripta za definiranje lokacije igraca na ulasku u prostor

    public ProstorEnums.Lokacija lokacijaSkripte;

    public Dictionary<string, List<string>> susjednaPodrucja = new Dictionary<string, List<string>>(); //definiranje susjednih podrucja za titlove i ostale stvari

    ExposureManager exposureManager;

    Titlovi titlovi;

    private void Start() {
        exposureManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ExposureManager>();
        titlovi = GameObject.FindGameObjectWithTag("Titlovi").GetComponent<Titlovi>();

        susjednaPodrucja.Add("Poljana", new List<string> {"TamnaSuma", "Jezero", "Brdo", "LovackaKuca", "Crkva", "VelikoDrvo"});
        susjednaPodrucja.Add("TamnaSuma", new List<string>());
        susjednaPodrucja.Add("Brdo", new List<string> {"TamnaSuma", "Poljana", "Jezero", "Crkva"});
        susjednaPodrucja.Add("Jezero", new List<string> {"Crkva", "Poljana", "Brdo"});
        susjednaPodrucja.Add("LovackaKuca", new List<string> {"VelikoDrvo", "kamenolom", "Spilja", "TamnaSuma", "Poljana"});
        susjednaPodrucja.Add("VelikoDrvo", new List<string>{"Poljana", "LovackaKuca", "Crkva"});
        susjednaPodrucja.Add("Spilja", new List<string> {"LovackaKuca", "TamnaSuma"});
        susjednaPodrucja.Add("Kamenolom", new List<string> {"LovackaKuca", "Spilja"});
        susjednaPodrucja.Add("Crkva", new List<string> {"Poljana", "Jezero", "VelikoDrvo", "Brdo"});



    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ProstorEnums.lokacijaIgraca != lokacijaSkripte) {
             //makni titlove od prijasnjeg podrucja
            titlovi.iskljuciTitlove(ProstorEnums.lokacijaIgraca.ToString());
            foreach (string lokacija in susjednaPodrucja[ProstorEnums.lokacijaIgraca.ToString()]) {
                titlovi.iskljuciTitlove(lokacija);
            }
            
            
            //Debug.Log("Usao " + lokacijaSkripte);
            ProstorEnums.lokacijaIgraca = lokacijaSkripte;
            exposureManager.promijenioLokaciju = true;

            //ukljuci sve titlove od ovog podrucja
            
            titlovi.ukljuciTitlove(lokacijaSkripte.ToString());

            if (lokacijaSkripte == ProstorEnums.Lokacija.Jezero && ProstorEnums.striborProgress == ProstorEnums.StriborProgression.NemaKljuca && !titlovi.iskoristeniBitniKljucevi.Contains("PrijeKljuca")) {
                titlovi.ukljuciTitlove("PrijeKljuca");
            }

            foreach (string lokacija in susjednaPodrucja[lokacijaSkripte.ToString()]) {
                //Debug.Log(lokacija);
                titlovi.ukljuciTitlove(lokacija);
            }
           

        }
       
    }
    
}
