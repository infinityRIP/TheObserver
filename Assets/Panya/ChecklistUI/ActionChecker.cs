using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections; // ต้องมีเพื่อใช้ List
using UnityEngine.SceneManagement;

public class ActionChecker : MonoBehaviour
{
    // ลาก Toggle ทั้งหมดจาก Hierarchy มาใส่ใน List นี้ผ่าน Inspector
    public List<Toggle> actionToggles;
    public CanvasGroup canvasGroup;
    public GameObject actionPanel;
    public float fadeDuration = 1f;

    // ลากปุ่มทั้งสองมาใส่
    public Button reportButton;
    public Button ignoreButton;

    // กำหนดเกณฑ์ในการแสดงปุ่ม
    public int decisionThreshold = 2;
    public int criticalThreshold = 4;
    public bool isDaypass;

    void Start()
    {
        actionPanel.SetActive(false); // แสดง Action Panel เมื่อเริ่มเกม
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Start the fade in

        // ซ่อนปุ่มในตอนเริ่มต้น
        reportButton.gameObject.SetActive(false);
        ignoreButton.gameObject.SetActive(true);

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
            Debug.Log("Toggle: " + toggle.name + " is " + (toggle.isOn ? "On" : "Off"));
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
            ignoreButton.gameObject.SetActive(true);
        }
    }

    public void ReportActions()
    {
        if (actionToggles[0].isOn && actionToggles[2].isOn && !actionToggles[1].isOn && !actionToggles[3].isOn)
        {
            Debug.Log("รายงานพฤติกรรมที่น่าสงสัย");
            // ที่นี่สามารถเพิ่มโค้ดเพื่อส่งข้อมูลไปยังระบบรายงานได้
        }
        StartCoroutine(FadeIn());



    }
    IEnumerator FadeIn() {
        actionPanel.SetActive(true); // แสดง Action Panel
        isDaypass = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        // Ensure it's fully visible and interactive
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(1f); // รอ 2 วินาที
        SceneManager.LoadScene("Dayscene"); // เปลี่ยนไปยัง Scene ที่ต้องการ
    }


}



