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
        if (DayManager.Instance.Day != 1)
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
        yield return new WaitForSeconds(2f); // กันไม่ให้สุ่มทันทีหลังเริ่ม

        while (true)
        {
            yield return new WaitForSeconds(1f);

            float chance = Random.Range(0f, 100f);

            if (chance < 90f && RE.NoPower == false && Bc.calling == false && isNowPlayingRandom == false)
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
        if (DayManager.Instance.Day != 1)
        {
            if (clips.Length == 0) return;

            int index = Random.Range(0, clips.Length);   // สุ่ม index

            AudioClip randomClip = clips[index];

            Pc.Audio.Stop();
            audioSource.clip = randomClip;     // ตั้งค่า clip
            audioSource.Play();                // เล่นเสียง
            isNowPlayingRandom = true;

            Debug.Log("Playing clip: " + randomClip.name);
        }
       
    }
}
