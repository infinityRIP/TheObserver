using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Collections;

public class Textanim : MonoBehaviour
{
    public float fallspeed = 100f;
    public TMP_Text number1;
    public TMP_Text number2;

    public float limit1 = -500f; // Y limit for number1
    public float limit2 = -300f; // Y limit for number2

    private bool isFalling = false;

    RectTransform rect1;
    RectTransform rect2;

    void Start()
    {
        rect1 = number1.GetComponent<RectTransform>();
        rect2 = number2.GetComponent<RectTransform>();

        StartCoroutine(FallingDelay());
    }

    void Update()
    {
        // number1 falls immediately
        if (rect1.anchoredPosition.y > limit1)
        {
            Vector2 pos = rect1.anchoredPosition;
            pos.y -= fallspeed * Time.deltaTime;
            pos.y = Mathf.Max(pos.y, limit1); // Clamp
            rect1.anchoredPosition = pos;
        }

        // number2 falls after delay
        if (isFalling && rect2.anchoredPosition.y > limit2)
        {
            Vector2 pos = rect2.anchoredPosition;
            pos.y -= fallspeed * Time.deltaTime;
            pos.y = Mathf.Max(pos.y, limit2); // Clamp
            rect2.anchoredPosition = pos;
        }
    }

    IEnumerator FallingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isFalling = true;
    }
}
