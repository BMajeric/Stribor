using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gledajScript : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 1, 0) );
    }
}
