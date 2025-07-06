using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VDOend : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad = "BombScene"; // ���� Scene ������Ŵ��ѧ���Դ���

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
