using System.Collections;
using TMPro;
using UnityEngine;

public class Power : MonoBehaviour
{
    public AudioSource NoPower;
    public AudioSource GenSound;
    public GameObject Screen;
    public GameObject Rick;
    public Light light1;
    public Light light2;
    public Light light3;

    public Light Light;
    public TMP_Text PPText;
    public RandomEvent RnEv;
    public int PowerPoint = 100;
    public float TnotRick = 2f;
    public float TRick = 0.5f;
    public bool isGen = false;
    void Start()
    {
        light3.enabled = false;
        Light.intensity = 0.2f;
        StartCoroutine(DecreaseEverySecond());
        UpdateText();
    }

    private void Update()
    {
    }

    IEnumerator DecreaseEverySecond()
    {
        while (PowerPoint > 0 )
        {
            if (isGen == false)
            {
                float waitTime = RnEv.isPowerFast ? TRick : TnotRick; // ถ้าเปิด ลดเร็วขึ้น

                yield return new WaitForSeconds(waitTime);
                PowerPoint--;
                UpdateText();
                GenSound.Stop(); // Stop the generator sound when power is not being generated
            } else if (isGen == true)
            {
                yield return new WaitForSeconds(0.2f);
                PowerPoint = Mathf.Min(PowerPoint + 1, 100);
                UpdateText();
                if (!GenSound.isPlaying) {
                    GenSound.PlayOneShot(GenSound.clip); // Play the generator sound when power is generated

                }
            }
        }
        if (PowerPoint <= 0)
        {
            PPText.text = "No Power!";
            NoPower.Play();
            Light.intensity = 0f;
            light1.enabled = false;
            light2.enabled = false;
            light3.enabled = true;
            Screen.SetActive(false);
            Rick.SetActive(false);
            RnEv.NoPower = true;
            yield break;
        }
    }
    void UpdateText()
    {
        PPText.text = "Power: " + PowerPoint.ToString() +  "%";
    }
}
