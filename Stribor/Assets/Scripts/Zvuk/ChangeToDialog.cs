using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToDialog : MonoBehaviour
{
    public Animator animator;

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene("Dialog");
    }
}
