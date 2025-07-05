using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mousescript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Transform screen; // the screen or camera pivot
    public float ySpeed = 20f; // degrees per second
    public GameObject Rightbutton; // Reference to the left button

    private bool isHover = false;
    private float currentY;
    public float resetSpeed = 20f;


    void Start()
    {
        // Initialize currentY with the current y-angle of the screen
        currentY = screen.eulerAngles.y;
        if (currentY > 180f) currentY -= 360f; // Normalize to -180...180 range
    }

    void Update()
    {
        if (isHover)
        {
            Rightbutton.SetActive(false); // Hide the right button when hovering over the left button
            // Increment the currentY angle
            currentY -= ySpeed * Time.deltaTime;

            // Clamp the angle to the maximum of -170 degrees
            currentY = Mathf.Max(currentY, 80f);

            // Apply the rotation if it's less than 170 degrees
            if (currentY > -80f)
            {
                // Apply the rotation
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        else
        {
            if (currentY <= 90)
            {
 
                currentY = Mathf.MoveTowards(currentY, 90f, resetSpeed * Time.deltaTime);
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }



        }
        if (currentY == 90f)
        {
            Rightbutton.SetActive(true); // Show the left button when the screen is at 180 degrees
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if(e.pointerEnter != null && e.pointerEnter.gameObject.name == "Left")
        {
            isHover = true; // Set the hover flag to true when the pointer enters
        }
    }
    public void OnPointerExit(PointerEventData e) => isHover = false;
}


