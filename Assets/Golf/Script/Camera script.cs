using UnityEngine;
using UnityEngine.UI;

public class Camerascript : MonoBehaviour
{
    bool isCameraZoom = false; // Flag to check if the camera has been reset
    public Camera mainCamera; // Reference to the main camera
    public Button Screenzoom; // Reference to the reset button
    public GameObject computerscreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraZoom == true)
        {
            computerscreen.SetActive(true); // Show the computer screen when camera is zoomed in
            Screenzoom.gameObject.SetActive(false); // Show the reset button when camera is zoomed in
        }
        else
        {
            computerscreen.SetActive(false); // Hide the computer screen when camera is not zoomed in
            Screenzoom.gameObject.SetActive(true); // Show the reset button when camera is not zoomed in
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isCameraZoom == true)
        {
            mainCamera.transform.position = new Vector3(0, 1.5f, 2.63f); // Reset camera position
            Debug.Log("Camera position reset to (0, 1.5, 1.8)");
            isCameraZoom = false; // Set the flag to false
        }

    }
    public void clickbutton()
    {
        mainCamera.transform.position = new Vector3(-0.049f, 1.568f, 1.461f); // Reset camera position
        Debug.Log("Camera position reset to (0, 1.5, 1.8)");
        isCameraZoom = true; // Set the flag to true
    }
}
