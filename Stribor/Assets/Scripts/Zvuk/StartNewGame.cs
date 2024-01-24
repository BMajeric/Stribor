using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class StartNewGame : MonoBehaviour
{

    
    public GameObject image;
    public Animator animatator;
    public void LoadNewGame()
    {
        StartCoroutine(DelayThenFall(2f));
        image.SetActive(true);
        Debug.Log(image.activeInHierarchy);
        animatator.Play("MenuFade", 0, 0.0f);
    }

    IEnumerator DelayThenFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Intro");
    }
}

