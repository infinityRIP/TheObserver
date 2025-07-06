using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public string sceneNameToLoad = "Ending"; // ตั้งชื่อ Scene ที่ต้องการเปลี่ยน

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
