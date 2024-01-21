using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
   public Animator animator;
   public GameObject menuOptions;
    public void OptionsGo(){
        animator.SetBool("prebaci", true);
        StartCoroutine(Upali());
        

    }

    IEnumerator Upali(){

        yield return new WaitForSeconds(3);
        menuOptions.SetActive(true);
    }

    public void OptionsBack(){
        menuOptions.SetActive(false);
        animator.SetBool("prebaci", false);
    }
}
