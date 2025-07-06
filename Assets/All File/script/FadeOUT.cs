using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public GameObject fadeOuutpanel;
    public CanvasGroup panelGroup;     // Drag your panel's CanvasGroup here
    public float fadeDuration = 1f;    // Duration in seconds

    void Start()
    {
        fadeOuutpanel.SetActive(true); // Ensure the panel is active
        panelGroup.alpha = 1f; // Ensure the panel starts fully visible
        // Start the fade out when the scene starts
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float startAlpha = panelGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }
        fadeOuutpanel.SetActive(false);
        panelGroup.alpha = 0f;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
    }

}
