using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class Screen2Script : MonoBehaviour
{
    public RandomEvent randomEvent;
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
        if (randomEvent != null)
        {
            randomEvent.isPowerFast = false;
        }
        VideoPlayer.SetActive(false);
        ToggleVideo();
    }
}
    
