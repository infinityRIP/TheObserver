using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // ต้องมีเพื่อใช้ List

public class ActionChecker : MonoBehaviour
{
    // ลาก Toggle ทั้งหมดจาก Hierarchy มาใส่ใน List นี้ผ่าน Inspector
    public List<Toggle> actionToggles;

    // ลากปุ่มทั้งสองมาใส่
    public Button reportButton;
    public Button ignoreButton;

    // กำหนดเกณฑ์ในการแสดงปุ่ม
    public int decisionThreshold = 2;
    public int criticalThreshold = 4;

    void Start()
    {
        // ซ่อนปุ่มในตอนเริ่มต้น
        reportButton.gameObject.SetActive(false);
        ignoreButton.gameObject.SetActive(false);

        // เพิ่ม Listener ให้กับ Toggle ทุกตัว
        // เมื่อมีการติ๊ก/ไม่ติ๊ก จะเรียกฟังก์ชัน CheckActions()
        foreach (Toggle toggle in actionToggles)
        {
            toggle.onValueChanged.AddListener(delegate { CheckActions(); });
        }
    }

    private void CheckActions()
    {
        int suspiciousCount = 0;
        foreach (Toggle toggle in actionToggles)
        {
            if (toggle.isOn)
            {
                suspiciousCount++;
            }
        }

        // Debug.Log("Suspicious actions: " + suspiciousCount); // ใช้เช็คค่า

        // ตรวจสอบว่าถึงเกณฑ์ที่กำหนดหรือไม่
        if (suspiciousCount >= decisionThreshold)
        {
            // ถ้ามีพฤติกรรมน่าสงสัย 3 ข้อขึ้นไป ให้แสดงปุ่ม
            reportButton.gameObject.SetActive(true);
            ignoreButton.gameObject.SetActive(true);

            if (suspiciousCount >= criticalThreshold)
            {
                Debug.Log("หลักฐานหนาแน่นมาก! ต้องตัดสินใจแล้ว");
                // อาจจะเพิ่ม Effect พิเศษ เช่น ปุ่มสั่น หรือเปลี่ยนสี
            }
        }
        else
        {
            // ถ้าน้อยกว่า 3 ให้ซ่อนปุ่มไว้
            reportButton.gameObject.SetActive(false);
            ignoreButton.gameObject.SetActive(false);
        }
    }
}