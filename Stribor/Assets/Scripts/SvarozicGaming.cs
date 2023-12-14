using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SvarozicGaming : MonoBehaviour
{

    //za palit ili gasit svijetlo
    public KeyCode LightGumb = KeyCode.R;

    public KeyCode BaciSvarozicaGumb = KeyCode.Mouse0;

    //koliko ih se moze bacati
    public int BrojSvarozica = 2;

    public GameObject SvarozicPrefab;

    //igrac ce moci ici do kamina da ojaca svijetlo svarozica
    public int snagaSvarozica = 1;

    public TMP_Text SvaroziciTekst;

    //lokacija s kojeg ce bacat svarozice
    public Transform startLokacija;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
