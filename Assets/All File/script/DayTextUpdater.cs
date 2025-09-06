using TMPro;
using UnityEngine;
using System.Collections;

public class DayTextUpdater : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // รอให้ Instance และ DayText ถูก Assign

        TMP_Text text = GetComponent<TMP_Text>();
        if (text != null && DayManager.Instance != null)
        {
            Debug.Log("Updating DayText to: " + DayManager.Instance.Day);
            text.text = "Day: " + DayManager.Instance.Day.ToString();
        }
        else
        {
            Debug.LogWarning("Text or DayManager not found.");
        }
    }
}
