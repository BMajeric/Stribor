using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvarozicInWorldSkripta : MonoBehaviour
{
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<Outline>().enabled = false;

        rigid = gameObject.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        //kada udaris nesto iskljuci gravitaciju i prestani se kretati

        //osim igraca
        if (other.gameObject.tag != "Player") {
            rigid.velocity = new Vector3(0f, 0f, 0f);

            rigid.useGravity = false;

            rigid.isKinematic = true;

            gameObject.GetComponent<Outline>().enabled = true;

            //radi neku animaciju kasnije ili nesto
        }

        
    }
}
