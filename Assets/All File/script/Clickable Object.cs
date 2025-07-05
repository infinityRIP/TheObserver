using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClickableObject : MonoBehaviour
{
    public Camerascript CS;
    public VideoPlayer videoPlayer;
    void Start()
    {
    }
    public void OnMouseDown()
    {
        if (CS != null)
        {
            CS.Zoom();
            ToggleVideo();
        }
    }
    public void ToggleVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}
