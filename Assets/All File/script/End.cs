using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public string sceneNameToLoad = "Ending"; // ��駪��� Scene ����ͧ�������¹

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
