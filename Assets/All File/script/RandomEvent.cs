using UnityEngine;
using System.Collections;
public class RandomEvent : Screen2Script
{
    public Power Pw;
    public bool NoPower = false;
    public Blinkingcode BC;

    void Start()
    {
        if (DayManager.Instance.Day == 2)
        {
            BC.hasStopped = true;
        }
        StartCoroutine(RandomEventLoop());
    }
    IEnumerator RandomEventLoop()
    {
        VP.Prepare();
        while (true)
        {
            yield return new WaitForSeconds(10f);

            float chance = Random.Range(0f, 100f);

            if (chance < 10f && NoPower == false && BC.hasStopped == true)
            {
                isPowerFast = true;
                VideoPlayer.SetActive(true);
                ToggleVideo();
            }
        }
    }
}
