using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement; // Required for scene management in Unity Editor
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Camerascript : MonoBehaviour
{
    [Header("Transform")]
    public GameObject StartPoint;
    public GameObject Zoom1;
    public GameObject Zoom2;
    public Vector3 PoStart;
    public Vector3 PoZoom1;
    public Vector3 PoZoom2;
    public Vector3 CurrentPo;
    float elapsedTime = 0f;
    float duration = 0.3f; //
    [Header("Cam")]
    public bool isCameraZoom = false; // Flag to check if the camera has been reset
    public Camera mainCamera; // Reference to the main camera
    [Header("Button")]
    public GameObject backbutton; // Reference to the back button
    public GameObject leftbutton; // Reference to the left button
    public GameObject rightbutton; // Reference to the right button
    public GameObject maingamecanvas; // Reference to the main game canvas
    [Header("Value")]
    public float rotateSpeed = 100f; // Speed of the camera rotation
    public int Fov = 90;
    public GameObject Panelfade; // Reference to the reset button
    public CanvasGroup myCanvasGroup;
    float camy;
    bool isZoom;
    void Start()
    {
        PoStart = StartPoint.transform.position;
        PoZoom1 = Zoom1.transform.position;
        PoZoom2 = Zoom2.transform.position;

        maingamecanvas.SetActive(true); // Show the main game canvas at the start
        myCanvasGroup.alpha = 0f;
        camy = mainCamera.transform.rotation.eulerAngles.y; // Store the initial y rotation of the camera
    }
    private void Update()
    {
    }
    public void Zoom()
    {
        CurrentPo = mainCamera.transform.position;
        StartCoroutine(ResetYRotation()); // Start the coroutine to reset the camera's y rotation
        StartCoroutine(Zoomcamera()); // Start the coroutine to zoom the camera
    }
    public void OutZoom()
    {
        CurrentPo = mainCamera.transform.position;
        StartCoroutine(ResetYRotation());
        StartCoroutine(Backcamera()); // Start the coroutine to zoom the camera
    }
    public IEnumerator Zoomcamera()
    {
        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(CurrentPo, PoZoom1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = PoZoom1;
        elapsedTime = 0;

    }
    public IEnumerator Backcamera()
    {
        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(CurrentPo, PoStart, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = PoStart;
        elapsedTime = 0;
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
}