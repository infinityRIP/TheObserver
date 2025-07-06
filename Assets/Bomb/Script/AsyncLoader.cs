using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    [Header("Loading Settings")]
    [SerializeField] private float minLoadTime = 2.0f; // Minimum time the loading screen will be displayed
    [SerializeField] private float sliderSpeed = 1f;   // Speed at which the slider animates

    public void loadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        float elapsedTime = 0f;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;

        // --- Phase 1: Animate based on actual loading progress ---
        // This loop runs as long as the scene is loading in the background.
        while (loadOperation.progress < 0.9f)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the target progress (from 0.0 to 1.0)
            float targetProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            // **KEY CHANGE**: Smoothly move the slider towards the target progress.
            // This prevents the slider from jumping instantly. `sliderSpeed` controls how fast it moves.
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, targetProgress, Time.deltaTime * sliderSpeed);

            yield return null;
        }

        // --- Phase 2: Fill the remainder and enforce minimum wait time ---
        // This loop ensures the slider reaches 100% and the loading screen
        // is displayed for at least 'minLoadTime'.
        while (elapsedTime < minLoadTime || loadingSlider.value < 1f)
        {
            elapsedTime += Time.deltaTime;

            // Continue animating the slider to its final value of 1.
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, Time.deltaTime * sliderSpeed);

            yield return null;
        }

        // Allow the newly loaded scene to activate.
        loadOperation.allowSceneActivation = true;
    }
}