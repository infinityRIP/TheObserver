using System.Collections; // ต้องมีเพื่อใช้ List
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionChecker : MonoBehaviour
{
    public DayManager DM;

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
    public bool IsActionCorrect()
    {
        bool[] currentToggleStates = new bool[actionToggles.Count];

        for (int i = 0; i < 4; i++)
        {
            currentToggleStates[i] = actionToggles[i].isOn;
        }

        return DM.selectedChecklistData.MatchesToggles(currentToggleStates);
    }

    void Start()
    {
        Debug.Log("Day " + DayManager.Instance.Day);
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
        CheckChecklist();
        StartCoroutine(FadeIn());

        DayManager.Instance.Day += 1;
        Debug.Log("Day" + DayManager.Instance.Day);
        DayManager.Instance.End();
    }


    IEnumerator FadeIn() {
        actionPanel.SetActive(true); // แสดง Action Panel

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

    public void CheckChecklist()
    {
        if (DM.ID == 1)
        {
            //DM.selectedChecklistData.Check1 == true && DM.selectedChecklistData.Check3 == true
            if (IsActionCorrect())
            {
                switch (DayManager.Instance.Day)
                {
                    case 1:
                        Daycorrectchecker.correct.isCorrectDay1 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P สำเร็จในวันแรก");
                        break;
                    case 2:
                        Daycorrectchecker.correct.isCorrectDay2 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P สำเร็จในวันที่สอง");
                        break;
                    case 3:
                        Daycorrectchecker.correct.isCorrectDay3 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P สำเร็จในวันที่สาม");
                        break;
                    case 4:
                        Daycorrectchecker.correct.isCorrectDay4 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P สำเร็จในวันที่สาม");
                        break;
                    case 5:
                        Daycorrectchecker.correct.isCorrectDay5 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P สำเร็จในวันที่สาม");
                        break;
                    default:
                        Debug.Log("ไม่พบข้อมูลสำหรับวันนี้");
                        break;
                }

            }
            else
            {
                switch (DayManager.Instance.Day)
                {
                    case 1:
                        Daycorrectchecker.correct.isCorrectDay1 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P ไม่สำเร็จในวันแรก");
                        break;
                    case 2:
                        Daycorrectchecker.correct.isCorrectDay2 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P ไม่สำเร็จในวันที่สอง");
                        break;
                    case 3:
                        Daycorrectchecker.correct.isCorrectDay3 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P ไม่สำเร็จในวันที่สาม");
                        break;
                    case 4:
                        Daycorrectchecker.correct.isCorrectDay4 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P ไม่สำเร็จในวันที่สาม");
                        break;
                    case 5:
                        Daycorrectchecker.correct.isCorrectDay5 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.P ไม่สำเร็จในวันที่สาม");
                        break;
                    default:
                        Debug.Log("ไม่พบข้อมูลสำหรับวันนี้");
                        break;
                }
            }

        }
        if (DM.ID == 2)
        {
            if (IsActionCorrect())
            {
                switch (DayManager.Instance.Day)
                {
                    case 1:
                        Daycorrectchecker.correct.isCorrectDay1 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T สำเร็จในวันแรก");
                        break;
                    case 2:
                        Daycorrectchecker.correct.isCorrectDay2 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T สำเร็จในวันที่สอง");
                        break;
                    case 3:
                        Daycorrectchecker.correct.isCorrectDay3 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T สำเร็จในวันที่สาม");
                        break;
                    case 4:
                        Daycorrectchecker.correct.isCorrectDay4 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T สำเร็จในวันที่สาม");
                        break;
                    case 5:
                        Daycorrectchecker.correct.isCorrectDay5 = true;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T สำเร็จในวันที่สาม");
                        break;
                    default:
                        Debug.Log("ไม่พบข้อมูลสำหรับวันนี้");
                        break;
                }

            }
            else
            {
                switch (DayManager.Instance.Day)
                {
                    case 1:
                        Daycorrectchecker.correct.isCorrectDay1 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T ไม่สำเร็จในวันแรก");
                        break;
                    case 2:
                        Daycorrectchecker.correct.isCorrectDay2 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T ไม่สำเร็จในวันที่สอง");
                        break;  
                    case 3:
                        Daycorrectchecker.correct.isCorrectDay3 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T ไม่สำเร็จในวันที่สาม");
                        break;
                    case 4:
                        Daycorrectchecker.correct.isCorrectDay4 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T ไม่สำเร็จในวันที่สาม");
                        break;
                    case 5:
                        Daycorrectchecker.correct.isCorrectDay5 = false;
                        Debug.Log("รายงานการกระทำที่น่าสงสัยของ Mr.T ไม่สำเร็จในวันที่สาม");
                        break;
                    default:
                        Debug.Log("ไม่พบข้อมูลสำหรับวันนี้");
                        break;
                }
            }

        }
    }
}



