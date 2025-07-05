using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class Canvas : MonoBehaviour {

    public string nextSceneName = "end";
   
    public void PlayGame ()
    {
        
        SceneManager.LoadScene(nextSceneName);
    }

}