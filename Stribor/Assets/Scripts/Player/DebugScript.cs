using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            start.GetComponent<ZapocniIgru>().enabled = true;
        }
    }

}
