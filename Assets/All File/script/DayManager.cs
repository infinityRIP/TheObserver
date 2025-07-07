using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public Camerascript Cs;
    public ActionChecker Ac;
    [Header("VDOData")]
    public VDOData[] allVDOData;
    private VDOData selectedVDOData;
    private VDOData selectedChecklistData;
    public Transform uiCanvasParent;
    public static DayManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (allVDOData.Length == 0)
        {
            Debug.LogWarning("ยังไม่มี VDOData ให้สุ่มเลย!");
            return;
        }
        ShowRandomVDOAndChecklist();

    }

    public void ShowRandomVDOAndChecklist()
    {
        if (allVDOData.Length == 0 || uiCanvasParent == null) return;

        // สุ่ม ScriptableObject ตัวเดียว
        selectedVDOData = allVDOData[Random.Range(0, allVDOData.Length)];
        selectedChecklistData = selectedVDOData; // ใช้ตัวเดียวกัน

        Debug.Log("สุ่มได้: " + selectedVDOData.name);

        // Instantiate วิดีโอ
        if (selectedVDOData.videoPrefab != null)
        {
            Instantiate(selectedVDOData.videoPrefab, transform);
        }

        // Instantiate Checklist UI
        if (selectedChecklistData.ChecklistPrefab != null)
        {
            GameObject checklistUI = Instantiate(selectedChecklistData.ChecklistPrefab, uiCanvasParent);
            checklistUI.SetActive(false); // ซ่อนไว้ก่อน หรือเปิดเลยตามต้องการ
            Cs.ChecklistToggle = checklistUI;

            Button[] buttons = checklistUI.GetComponentsInChildren<Button>(true);

            foreach (Button btn in buttons)
            {
                if (btn.name == "ignore")
                {
                    Ac.ignoreButton = btn;

                }
                else if (btn.name == "Report")
                {
                    Ac.reportButton = btn;
                    Ac.reportButton.onClick.AddListener(() => Ac.ReportActions());
                }
            }

            if (Ac.ignoreButton == null)
                Debug.LogWarning("ไม่พบปุ่ม Ignore ใน ChecklistPrefab");
            else
                Ac.ignoreButton.onClick.AddListener(() => Ac.ReportActions());
            if (Ac.reportButton == null)
                Debug.LogWarning("ไม่พบปุ่ม Report ใน ChecklistPrefab");
            else
                Ac.reportButton.onClick.AddListener(() => Ac.ReportActions());

            Toggle[] toggles = checklistUI.GetComponentsInChildren<Toggle>(true);
            Ac.actionToggles = new List<Toggle>(toggles);

            //เพิ่ม Listener ให้ Toggle ทุกตัว (เหมือนใน Start())
            foreach (Toggle toggle in Ac.actionToggles)
            {
                toggle.onValueChanged.AddListener(delegate { Ac.SendMessage("CheckActions"); });
            }
        }

        Debug.Log("Description: " + selectedVDOData.description);
    }

}
