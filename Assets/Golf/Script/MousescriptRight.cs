using UnityEngine;
using UnityEngine.EventSystems;

public class MousescriptRight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform screen; // the screen or camera pivot
    public float ySpeed = 20f; // degrees per second

    private bool isHover = false;
    private float currentY;
    public float resetSpeed = 20f;


    void Start()
    {
        // Initialize currentY with the current y-angle of the screen
        currentY = screen.eulerAngles.y;
    }

    void Update()
    {
        if (isHover)
        {
            // Increment the currentY angle
            currentY += ySpeed * Time.deltaTime;
            currentY = Mathf.Max(currentY, 190f);

            if (currentY <= 190f)
            {
                // Apply the rotation
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        else
        {
            if (currentY >= 180)
            {

                currentY = Mathf.MoveTowards(currentY, 180f, resetSpeed * Time.deltaTime);
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }



        }
    }

    public void OnPointerEnter(PointerEventData e) => isHover = true;
    public void OnPointerExit(PointerEventData e) => isHover = false;
}
