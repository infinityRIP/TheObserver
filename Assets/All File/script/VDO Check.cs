using UnityEngine;
using UnityEngine.Video;

public class VDOCheck : MonoBehaviour
{
    [SerializeField] VideoPlayer Vdo;
    void Start()
    {
        Vdo.loopPointReached += EndVideo;
    }

    private void EndVideo(VideoPlayer source)
    {
        Debug.LogWarning("Video End!");
    }

}
