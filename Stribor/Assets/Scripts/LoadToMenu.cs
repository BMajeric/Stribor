using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(23f);
        SceneManager.LoadScene("main_menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
