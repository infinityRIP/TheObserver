using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class Screen2Script : MonoBehaviour
{
    public GameObject VideoPlayer;
    public VideoPlayer VP;
    public bool isPowerFast = false;
    void Start()
    {
        VP = VideoPlayer.GetComponent<VideoPlayer>();
        VideoPlayer.SetActive(false);
    }

    public void OnMouseDown()
    {
        StopVideo();
    }
    public void ToggleVideo()
    {
        if (VP.isPlaying)
        {
            VP.Pause();
        }
        else
        {
            VP.Play();
        }
    }
    public void StopVideo()
    {
        VideoPlayer.SetActive(false);
        ToggleVideo();
    }
}
    
