using System.Collections;
using UnityEngine;

public class RandomCall : MonoBehaviour
{
    public RandomEvent RE;

    public Blinkingcode Bc;

    public AudioSource audioSource;   
    public AudioClip[] clips;

    void Start()
    {
        StartCoroutine(RandomEventLoop());
    }
    IEnumerator RandomEventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            float chance = Random.Range(0f, 100f);

            if (chance < 20f && RE.NoPower == false && Bc.hasStopped == true)
            {
                PlayRandomSound();
            }
        }
    }

    public void PlayRandomSound()
    {
        if (clips.Length == 0) return;

        int index = Random.Range(0, clips.Length);   // ���� index
        AudioClip randomClip = clips[index];

        audioSource.clip = randomClip;     // ��駤�� clip
        audioSource.Play();                // ������§
    }
}
