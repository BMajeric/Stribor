using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class HideableObject : MonoBehaviour
{

    FirstPersonController playerController;

    public KeyCode hideKey = KeyCode.E;

    //za vratiti igraca
    private Vector3 oldPos;

    //za postaviti igraca
    public Transform hidingPos;

    BoxCollider playerCollider1;

    CapsuleCollider playerCollider2;

    public AudioSource audioSource;

    public AudioClip hideSound;

    ExposureManager exposureManager;

    bool isHiding;
    // Start is called before the first frame updatea
    void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        isHiding = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        playerCollider1 = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        playerCollider2 = GameObject.FindGameObjectWithTag("Player").transform.Find("Collider2").GetComponent<CapsuleCollider>();
        exposureManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ExposureManager>();
    }

    // Update is called once per frame

    public void Hide(Transform position) {

        //fali jos samo animacija

        oldPos = position.position;

        playerController.enableHeadBob = false;

        playerController.gameObject.transform.GetComponent<CustomGravity>().gravityScale = 0;

        playerController.gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        playerController.isWalking = false;

        playerController.playerCanMove[1] = false;

        playerController.enableCrouch = false;

        playerController.gameObject.transform.position = hidingPos.position;

        this.GetComponent<MeshCollider>().enabled = false;

        playerCollider1.enabled = false;
        playerCollider2.enabled = false;

        audioSource.PlayOneShot(hideSound);

        exposureManager.ExposureTarget -= 50;

        exposureManager.ExposureRate = 0.1f;

        StartCoroutine("Unhide");

    }

    private void Update() {
        
    }

    IEnumerator Unhide() {

        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetKeyDown(hideKey));
        
        isHiding = false;

        playerController.enableHeadBob = true;

        playerController.gameObject.transform.GetComponent<CustomGravity>().gravityScale = 2;

        playerController.gameObject.transform.position = oldPos;

        playerController.playerCanMove[1] = true;

        playerController.enableCrouch = true;

        this.GetComponent<MeshCollider>().enabled = true;

        playerCollider1.enabled = true;
        playerCollider2.enabled = true;

        audioSource.PlayOneShot(hideSound);

        exposureManager.ExposureTarget += 50;

        exposureManager.ExposureRate = 0.5f;


    }

    
}
