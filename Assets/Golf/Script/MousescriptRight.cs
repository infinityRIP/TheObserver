using UnityEngine;
using UnityEngine.EventSystems;

public class MousescriptRight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform screen;
    public GameObject leftbutton;
    public float ySpeed = 20f;
    public float resetSpeed = 1f;

    private bool isHover = false;
    private float currentY;

    void Start()
    {
        currentY = screen.eulerAngles.y;
    }

    void Update()
    {
        if (isHover)
        {
            Debug.Log($"isHover: {isHover}, currentY: {currentY}");
            // Increment the currentY angle
            currentY += ySpeed * Time.deltaTime;


            currentY = Mathf.Min(currentY, 100f);

            if (currentY > -170f)
            {
                // Apply the rotation
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        else
        {
            if (currentY >= 90f)
            {

                currentY = Mathf.MoveTowards(currentY, 90f, resetSpeed * Time.deltaTime);
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }



        }
        if (currentY == 90f)
        {
            leftbutton.SetActive(true); // Show the left button when the screen is at 180 degrees
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (e.pointerEnter != null && e.pointerEnter.gameObject.name == "Right")
        {
            leftbutton.SetActive(false); // Hide the left button when hovering over the right button
            isHover = true; // Set the hover flag to true when the pointer enters
        }
    }
    public void OnPointerExit(PointerEventData e) {
        isHover = false; // Set the hover flag to false when the pointer exits

    }
}
    



