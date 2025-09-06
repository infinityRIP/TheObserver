using System.Collections;
using UnityEngine;

public class RandomCall : MonoBehaviour
{
    public RandomEvent RE;
    public PhoneCall Pc;
    public Blinkingcode Bc;

    public AudioSource audioSource;   
    public AudioClip[] clips;
    private Coroutine randomEventCoroutine;

    public bool isNowPlayingRandom = false;

    void Start()
    {
        if (DayManager.Instance.Day >= 2)
        {
            Debug.LogWarning("Now Calling");
            randomEventCoroutine = StartCoroutine(RandomEventLoop());
        } 
    }
    public void Update()
    {
        if (!audioSource.isPlaying)
        {
            isNowPlayingRandom = false;
        }
           
    }
    IEnumerator RandomEventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            float chance = Random.Range(0f, 100f);

            if (chance < 20f && RE.NoPower == false && Bc.calling == false && isNowPlayingRandom == false)
            {
                Debug.LogWarning("Now Calling");
                Bc.calling = true;
                Pc.Audio.Play();
            }
        }
    }
    public void EndRandomCall()
    {
        isNowPlayingRandom = false;
        Bc.calling = false;
        Pc.Audio.Stop();


        if (audioSource.isPlaying)
            audioSource.Stop();

        if (randomEventCoroutine != null)
        {
            StopCoroutine(randomEventCoroutine);
        }

        randomEventCoroutine = StartCoroutine(RandomEventLoop());
    }

    public void PlayRandomSound()
    {
        if (DayManager.Instance.Day >= 2)
        {
            if (clips.Length == 0) return;

            int index = Random.Range(0, clips.Length);   // ���� index

            AudioClip randomClip = clips[index];

            Pc.Audio.Stop();
            audioSource.clip = randomClip;     // ��駤�� clip
            audioSource.Play();                // ������§
            isNowPlayingRandom = true;

            Debug.Log("Playing clip: " + randomClip.name);
        }
       
    }
}
