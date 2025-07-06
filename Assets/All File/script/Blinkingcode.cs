using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blinkingcode : MonoBehaviour
{
    public ActionChecker AC;
    public Renderer rend;
    public PhoneCall PC;
    public AudioSource Audio;
    public AudioSource Pickup;

    public float pulseSpeed = 2f; // How fast it pulses
    public bool calling;
    public bool isAudioPlay;
    public bool hasStopped = false;
    bool hasPick = false;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Main Day 2")
        {
            hasStopped = true;
        }
        calling = false;
        isAudioPlay = Audio.isPlaying;
        // Make sure emission is enabled
        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (calling == true)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);

            // Lerp from black to green based on t
            Color emissionColor = Color.Lerp(Color.black, Color.green, t);

            // Apply to emission
            rend.material.SetColor("_EmissionColor", emissionColor);
        }
        else
        {             // If not calling, set emission to black
            rend.material.SetColor("_EmissionColor", Color.black);
        }
        if (!Audio.isPlaying && !hasStopped && hasPick == true)
        {
            hasStopped = true;
        }
    }
    IEnumerator AfterDelay()
    {
        Pickup.Play();
        PC.Audio.Stop();
        calling = false;

        yield return new WaitForSeconds(2f);
        Audio.Play();
        hasPick = true;
            
    }

    public void OnMouseDown()
    {

         StartCoroutine(AfterDelay());

    }

}
