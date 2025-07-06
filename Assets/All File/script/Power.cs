using System.Collections;
using UnityEngine;
using TMPro;

public class Power : MonoBehaviour
{
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
            } else if (isGen == true)
            {
                yield return new WaitForSeconds(0.1f);
                PowerPoint = Mathf.Min(PowerPoint + 1, 100);
                UpdateText();
            }
        }
        if (PowerPoint <= 0)
        {
            PPText.text = "No Power!";
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
