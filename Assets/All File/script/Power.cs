using System.Collections;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class Power : MonoBehaviour
{
    public TMP_Text PPText;
    public RandomEvent RnEv;
    public int PowerPoint = 100;
    public bool isPowerFast = false;
    void Start()
    {
        StartCoroutine(DecreaseEverySecond());
        UpdateText();
    }

    void Update()
    {
    }

    IEnumerator DecreaseEverySecond()
    {
        while (PowerPoint > 0)
        {
            float waitTime = isPowerFast ? 0.3f : 1f;
            yield return new WaitForSeconds(2f);
            PowerPoint--;
            UpdateText();
            Debug.Log("Current value: " + PowerPoint);
        }
        if (PowerPoint <= 0)
        {
            PPText.text = "No Power!";
            yield break;
        }
    }
    void UpdateText()
    {
        PPText.text = "Power: " + PowerPoint.ToString() +  "%";
    }
}
