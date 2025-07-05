using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousescriptRight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Mousescript MSL;
    public Transform screen;
    public GameObject leftbutton;
    public float ySpeed = 20f;
    public float resetSpeed = 1f;

    private bool isHover = false;
    private float currentY;
    public float Times;

    void Start()
    {
        Times = Time.deltaTime;
        currentY = screen.eulerAngles.y;
    }

    void Update()
    {
        if (isHover)
        {
            currentY += ySpeed * Times;
            currentY = Mathf.Min(currentY, 180f);

            if (currentY > -170f)
            {
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        if (!isHover && currentY >= 90)
        {
            currentY -= ySpeed * Times;
        }
        {
            leftbutton.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (e.pointerEnter != null && e.pointerEnter.gameObject.name == "Right")
        {
            isHover = true; 
        }
    }
    public void OnPointerExit(PointerEventData e) 
    {
        isHover = false;
    }
}
    



