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

        if (other.transform.tag != "Player" && other.transform.tag != "Svarozic") {
            rigid.linearVelocity = new Vector3(0f, 0f, 0f);

            //rigid.useGravity = false;

            //rigid.isKinematic = true;
        }
        
        

        //this.GetComponent<MeshCollider>().enabled = false;



        //radi neku animaciju kasnije ili nesto
        

        
    }
}
