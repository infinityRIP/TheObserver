using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VDOend : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad = "BombScene"; // ชื่อ Scene ที่จะโหลดหลังจบวิดีโอ

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
