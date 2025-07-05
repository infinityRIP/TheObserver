using UnityEngine;
using UnityEngine.UI; // แก้ไขจาก UnityEngine.Ui เป็น UnityEngine.UI

public class endscripts : MonoBehaviour
{
    public float scrollSpeed = 70f;

    private RectTransform rectTransform; // แก้ไขจาก ReactTransform เป็น RectTransform

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // แก้ไขจาก ReactTransform เป็น RectTransform
        // ตรวจสอบว่าได้ RectTransform มาหรือไม่
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not found on this GameObject. Please ensure this script is attached to a UI element with a RectTransform component.");
            enabled = false; // ปิด script ถ้าไม่เจอ RectTransform
        }
    }

    void Update()
    {
        // คำนวณการเลื่อนในแกน Y
        float yMovement = scrollSpeed * Time.deltaTime;
        // เพิ่มตำแหน่งในแกน Y ให้กับ anchoredPosition
        rectTransform.anchoredPosition += new Vector2(0, yMovement); // แก้ไขการสร้าง Vector2 และการคำนวณ
    }
}