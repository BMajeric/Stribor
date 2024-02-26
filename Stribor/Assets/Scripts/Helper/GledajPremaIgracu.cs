using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GledajPremaIgracu : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        
    }
}
