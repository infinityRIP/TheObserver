using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneNavigation : MonoBehaviour
{
   
    public string previousSceneName = "end"; 

    
    public void GoBackToPreviousScene()
    {
        
        SceneManager.LoadScene(previousSceneName);
    }

    
    public string nextSceneName = "Bombscene"; 

    public void PlayGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

