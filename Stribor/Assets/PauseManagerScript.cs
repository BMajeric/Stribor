using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerScript : MonoBehaviour
{
    public GameObject jeleniceUI;
    public GameObject pauseUI;

    public GameObject camera;
    void Update(){
        //esc
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
            jeleniceUI.SetActive(false);
            pauseUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
			
    }

   



    public void StisniNe(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        jeleniceUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void StisniDa(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
}
