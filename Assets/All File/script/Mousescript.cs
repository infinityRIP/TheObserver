using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mousescript : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Camerascript Cs;
    public MousescriptRight MSR;
    public Transform screen; // the screen or camera pivot
    public float ySpeed = 100f; // degrees per second
    public GameObject Rightbutton; // Reference to the left button
    public float Times;

    private bool isHover = false;
    public float resetSpeed = 10f;
    public float currentY;


    void Start()
    {
        Times = Time.deltaTime;
        currentY = screen.eulerAngles.y;
    }

    void Update()
    {
        //Debug.Log($"Left Y : {currentY}");
        if (isHover && !Cs.isOnCheklist)
        {
            currentY -= ySpeed * Time.deltaTime;
            currentY = Mathf.Max(currentY, 0f);

            if (currentY > -70f)
            {
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        if (!isHover && currentY <= 90)
        {
            currentY += ySpeed * Time.deltaTime;
        }

    }

    public void OnPointerEnter(PointerEventData e)
    {
        if(e.pointerEnter != null && e.pointerEnter.gameObject.name == "Left")
        {
            isHover = true;
        }

    }
    public void OnPointerExit(PointerEventData e)
    {
        isHover = false;
    }
}


