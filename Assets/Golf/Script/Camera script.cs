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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraZoom == true)
        {
            Screenzoom.gameObject.SetActive(false); // Show the reset button when camera is zoomed in

        }
        else
        {
            Screenzoom.gameObject.SetActive(true); // Show the reset button when camera is not zoomed in


        }

        if (Input.GetKeyDown(KeyCode.Escape) && isCameraZoom == true)
        {
            mainCamera.transform.position = new Vector3(0, 1.5f, 2.63f); // Reset camera position
            Debug.Log("Camera position reset to (0, 1.5, 1.8)");
            isCameraZoom = false; // Set the flag to false
            maingamecanvas.SetActive(true); // Show the main game canvas when camera is reset
        }

    }
    public void clickbutton()
    {
        maingamecanvas.SetActive(false); // Hide the main game canvas when the button is clicked
        mainCamera.transform.position = new Vector3(0f, 1.568f, 1.461f); 
        Debug.Log("Camera position reset to (0, 1.5, 1.8)");
        isCameraZoom = true; // Set the flag to true
    }
}
