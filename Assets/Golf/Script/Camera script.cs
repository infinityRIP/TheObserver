using System.Collections;
using UnityEditor.SceneManagement; // Required for scene management in Unity Editor
using UnityEngine;
using UnityEngine.UI;

public class Camerascript : MonoBehaviour
{
    public bool isCameraZoom = false; // Flag to check if the camera has been reset
    public Camera mainCamera; // Reference to the main camera
    public Button Screenzoom; // Reference to the reset button
    public GameObject leftbutton; // Reference to the left button
    public GameObject rightbutton; // Reference to the right button
    public GameObject maingamecanvas; // Reference to the main game canvas
    public float zoomSpeed = 20f; // Speed of the camera zoom
    public float rotateSpeed = 100f; // Speed of the camera rotation
    public GameObject Panelfade; // Reference to the reset button
    public CanvasGroup myCanvasGroup;
    float camy;
    void Start()
    {
        maingamecanvas.SetActive(true); // Show the main game canvas at the start
        myCanvasGroup.alpha = 0f;
        camy = mainCamera.transform.rotation.eulerAngles.y; // Store the initial y rotation of the camera
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Camera position: " + mainCamera.transform.rotation.eulerAngles);
        if (isCameraZoom == true)
        {
            StartCoroutine(FadeOut()); // Start fading out the canvas group when camera is zoomed in
            StartCoroutine(ResetYRotation()); // Start the coroutine to reset the camera's y rotation
            Screenzoom.gameObject.SetActive(false); // Show the reset button when camera is zoomed in
            StartCoroutine(Zoomcamera()); // Start the coroutine to zoom the camera
            maingamecanvas.SetActive(false); // Hide the main game canvas when camera is zoomed in

        }
        else
        {
            Screenzoom.gameObject.SetActive(true); // Show the reset button when camera is not zoomed in


        }


    }
    public void clickbutton()
    {
        Panelfade.SetActive(true); // Show the panel fade when the button is clicked
        StartCoroutine(Zoomcamera());
        Debug.Log("Camera position reset to (0, 1.5, 1.8)");
        isCameraZoom = true; // Set the flag to true
    }
    public IEnumerator Zoomcamera()
    {
        mainCamera.fieldOfView = Mathf.MoveTowards(
        mainCamera.fieldOfView,
        10,
        zoomSpeed * Time.deltaTime);

        yield return new WaitForSeconds(2f);

        Debug.Log("change scene");



    }
    IEnumerator ResetYRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(
            mainCamera.transform.eulerAngles.x,
            90f,
            mainCamera.transform.eulerAngles.z
        );

        while (Quaternion.Angle(mainCamera.transform.rotation, targetRotation) > 0.1f)
        {
            mainCamera.transform.rotation = Quaternion.RotateTowards(
                mainCamera.transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            );
            yield return null;
        }

        mainCamera.transform.rotation = targetRotation; // snap to final angle
    }
    IEnumerator FadeOut()
    {
        float fadeDuration = 1f; // Duration of the fade out
        float elapsed = 0f;
        while (elapsed <= fadeDuration)
        {
            elapsed += Time.deltaTime;
            myCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        myCanvasGroup.alpha = 1f;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }

}