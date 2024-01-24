using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runeUpali : MonoBehaviour
{
    public int brojjelenica;
    [SerializeField] public List<GameObject> rune;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        brojjelenica = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastingForItems>().brojJelenica;
     
        if (brojjelenica == 10){
            for(int i = 0; i < rune.Count ; i++){
                rune[i].SetActive(true);
            }
        }
    }
}
