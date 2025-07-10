using UnityEngine;
using System.Collections;

public class PhoneCall : MonoBehaviour
{

    public Blinkingcode Bc;

    public AudioSource Audio;

    private bool isPlayed = false;

    void Start()
    {
        if (DayManager.Instance.Day == 1)
        {
            StartCoroutine(PlayAfterDelay());
        }
        
    }

    IEnumerator PlayAfterDelay()
    {
        if (isPlayed == false)
        {
            yield return new WaitForSeconds(2f);
            isPlayed = true;
            Bc.calling = true;
            Audio.Play();
        }
    }
}
