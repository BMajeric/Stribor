using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : MonoBehaviour
{

    FirstPersonController playerController;

    public KeyCode hideKey = KeyCode.E;

    //za vratiti igraca
    private Vector3 oldPos;

    //za postaviti igraca
    public Transform hidingPos;

    bool isHiding;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        isHiding = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame

    public void Hide(Transform position) {

        //fali jos samo animacija

        oldPos = position.position;

        playerController.enableHeadBob = false;

        playerController.gameObject.transform.GetComponent<CustomGravity>().gravityScale = 0;

        playerController.gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0);

        playerController.playerCanMove[1] = false;

        playerController.gameObject.transform.position = hidingPos.position;

        

        StartCoroutine("Unhide");

    }

    private void Update() {
        
    }

    IEnumerator Unhide() {

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetKeyDown(hideKey));
        
        isHiding = false;

        playerController.enableHeadBob = true;

        playerController.gameObject.transform.GetComponent<CustomGravity>().gravityScale = 2;

        playerController.gameObject.transform.position = oldPos;

        playerController.playerCanMove[1] = true;

    


    }

    
}
