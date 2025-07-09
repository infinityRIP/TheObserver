using System.Collections;
using UnityEngine;


public class Camerascript : MonoBehaviour
{
    [Header("Object")]
    public GameObject ChecklistToggle;
    [Header("Transform")]
    public GameObject StartPoint;
    public GameObject Zoom1;
    public GameObject Zoom2;
    public GameObject ZoomCL;
    public Vector3 PoStart;
    public Vector3 PoZoom1;
    public Vector3 PoZoom2;
    public Vector3 PoZoomCL;
    public Vector3 CurrentPo;
    public Quaternion RoStart;
    public Quaternion RoZoomCL;
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
    public bool isOnCheklist = false;
    void Start()
    {
        if (ZoomCL != null)
        {
            PoZoomCL = ZoomCL.transform.position;
            RoZoomCL = ZoomCL.transform.rotation;
        }
        else
        {
            Debug.LogError("ZoomCL is not assigned!");
        }
        if (StartPoint != null)
        {
            PoStart = StartPoint.transform.position;
            RoStart = StartPoint.transform.rotation;
        }
        else
        {
            Debug.LogError("StartPoint is not assigned!");
        }

        PoZoom1 = Zoom1.transform.position;

        maingamecanvas.SetActive(true); // Show the main game canvas at the start
        myCanvasGroup.alpha = 0f;
        camy = mainCamera.transform.rotation.eulerAngles.y; // Store the initial y rotation of the camera
    }
    public void Zoom()
    {
        CurrentPo = mainCamera.transform.position;
        StartCoroutine(ResetYRotation()); // Start the coroutine to reset the camera's y rotation
        StartCoroutine(Zoomcamera()); // Start the coroutine to zoom the camera
    }
    public void OutZoom()
    {
        isOnCheklist = false;
        ChecklistToggle.SetActive(false);
        CurrentPo = mainCamera.transform.position;
        StartCoroutine(ResetYRotation());
        StartCoroutine(Backcamera()); // Start the coroutine to zoom the camera
    }
    public void Checklist()
    {
        isOnCheklist = true;
        mainCamera.transform.position = ZoomCL.transform.position;
        mainCamera.transform.rotation = ZoomCL.transform.rotation;
        ChecklistToggle.SetActive(true);
        Debug.Log("Checklist Open!");
        CurrentPo = mainCamera.transform.position;
        StartCoroutine(ResetYRotation());
        mainCamera.transform.position = PoZoomCL;
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
            mainCamera.transform.rotation = StartPoint.transform.rotation;
            mainCamera.transform.position = Vector3.Lerp(CurrentPo, PoStart, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = PoStart;
        elapsedTime = 0;
    }

    IEnumerator ResetYRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x,90f,mainCamera.transform.eulerAngles.z);

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