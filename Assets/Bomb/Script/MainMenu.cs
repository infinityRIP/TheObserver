using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour {

    public string nextSceneName = "Main"; //������ҡ���
   
    public void PlayGame ()
    {
        
        SceneManager.LoadScene("Main");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}