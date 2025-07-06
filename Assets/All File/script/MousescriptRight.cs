using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousescriptRight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Mousescript MSL;
    public Transform screen;
    public GameObject leftbutton;
    public float ySpeed = 40f;
    public float resetSpeed = 1f;

    private bool isHover = false;
    public float Times;
    public float currentY;

    void Start()
    {
        Times = Time.deltaTime;
        currentY = screen.eulerAngles.y;
    }

    void Update()
    {
        //Debug.LogWarning($"Right Y : {currentY}");
        if (isHover)
        {
            currentY += ySpeed * Time.deltaTime;
            currentY = Mathf.Min(currentY, 180f);

            if (currentY > -170f)
            {
                screen.rotation = Quaternion.Euler(screen.eulerAngles.x, currentY, screen.eulerAngles.z);
            }
        }
        if (!isHover && currentY >= 90)
        {
            currentY -= ySpeed * Time.deltaTime;
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
    



