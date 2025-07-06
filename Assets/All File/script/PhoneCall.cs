using UnityEngine;
using System.Collections;

public class PhoneCall : MonoBehaviour
{

    public Blinkingcode Bc;

    public AudioSource Audio;

    private bool isPlayed = false;

    void Start()
    {
        StartCoroutine(PlayAfterDelay());
    }

    IEnumerator PlayAfterDelay()
    {
        if (isPlayed == false)
        {
            yield return new WaitForSeconds(2f);
            Audio.Play();
            isPlayed = true;
            Bc.calling = true;
        }
    }
}
