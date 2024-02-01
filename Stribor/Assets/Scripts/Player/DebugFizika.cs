using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFizika : MonoBehaviour
{
    // Start is called before the first frame update

    public bool grounded;

    public float slope;

    FirstPersonController fpc;
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
