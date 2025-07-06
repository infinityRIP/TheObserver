using UnityEngine;
using UnityEngine.Video;
using System.Collections;
public class RandomEvent : Screen2Script
{
    public Power Pw;    
    void Start()
    {
        StartCoroutine(RandomEventLoop());
    }
    IEnumerator RandomEventLoop()
    {
        VP.Prepare();
        while (true)
        {
            yield return new WaitForSeconds(5f);

            float chance = Random.Range(0f, 100f);

            if (chance < 30f)
            {
                Pw.isPowerFast = true;
                VideoPlayer.SetActive(true);
                ToggleVideo();
            }
        }
    }
}
