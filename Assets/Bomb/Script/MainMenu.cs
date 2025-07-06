using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour {

    public string nextSceneName = "Main"; //������ҡ���
   
    public void PlayGame ()
    {
        
        SceneManager.LoadScene(nextSceneName);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}