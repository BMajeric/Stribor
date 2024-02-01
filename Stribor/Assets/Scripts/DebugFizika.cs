using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFizika : MonoBehaviour
{
    public bool grounded;
    public float slope;

    FirstPersonController fpc;
    // Start is called before the first frame update
    void Start()
    {
        fpc = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = fpc.isGrounded;
        slope = fpc.slopeAngle;
        
    }
}
