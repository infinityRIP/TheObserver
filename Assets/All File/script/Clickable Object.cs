using UnityEngine;
using UnityEngine.Video;

public class ClickableObject : MonoBehaviour
{
    public Camerascript CS;
    public VideoPlayer videoPlayer;
    public void OnMouseDown()
    {
        if (CS != null)
        {
            CS.Zoom();
            ToggleVideo();
        }
        else Debug.LogError("No CameraScript");

        if (videoPlayer != null)
        {

        }
        else Debug.LogError("No CameraScript");

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
