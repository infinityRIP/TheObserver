using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Textanim : MonoBehaviour
{
    public AudioSource DayChange;
    public float fallspeed = 100f;
    public TMP_Text number1;
    public TMP_Text number2;
    public TMP_Text tmpText;
    public TMP_Text tmpText2;
    public float fadeDuration = 1f;
    public float limit1 = -3000f; // Y limit for number1
    public float limit2 = -300f; // Y limit for number2
    public bool FinishFade =false;
    public CanvasGroup panelGroup;

    private bool isFalling = false;

    RectTransform rect1;
    RectTransform rect2;

    void Start()
    {
        DayChange.Play();
        panelGroup.alpha = 0f;
        rect1 = number1.GetComponent<RectTransform>();
        rect2 = number2.GetComponent<RectTransform>();
        StartCoroutine(FadeIN2());
        StartCoroutine(FadeOut());
        StartCoroutine(FallingDelay());

    }

    void Update()
    {
        // number1 falls immediately
        if (rect1.anchoredPosition.y > limit1 && FinishFade == true)
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
        yield return new WaitForSeconds(1f);
        isFalling = true;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2.5f); // Wait for 1 second before starting the fade out
        Color originalColor = tmpText.color;
        Color originalColor2 = tmpText2.color;
        float alpha = originalColor.a;
        float alpha2 = originalColor2.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(alpha, 1f, elapsed / fadeDuration);
            float newAlpha2 = Mathf.Lerp(alpha2, 1f, elapsed / fadeDuration);
            tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            tmpText2.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, newAlpha2);
            yield return null;
        }

        // Make sure it's fully invisible at the end
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        tmpText2.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        SceneManager.LoadScene("Day 2"); // Change to the desired scene
    }
    IEnumerator FadeIN()
    {
        Color originalColor = tmpText.color;
        Color originalColor2 = tmpText2.color;
        float alpha = originalColor.a;
        float alpha2 = originalColor2.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(alpha, 0f, elapsed / fadeDuration);
            float newAlpha2 = Mathf.Lerp(alpha2, 0f, elapsed / fadeDuration);
            tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            tmpText2.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, newAlpha2);
            yield return null;
        }

        // Make sure it's fully invisible at the end
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        tmpText2.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        SceneManager.LoadScene("Day 2"); // Change to the desired scene
        FinishFade = true;

    }
    public IEnumerator FadeIN2()
    {
        float startAlpha = panelGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsed / fadeDuration);
            yield return null;
        }
        panelGroup.alpha = 1f;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
        FinishFade = true; // Set to true to allow falling

    }

}
