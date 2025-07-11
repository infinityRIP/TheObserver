using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [Header("Object")]
    public GameObject EventSystem;
    private GameObject currentVideoObj;
    private GameObject currentChecklistUI;
    public bool isMrP = false;
    public bool isMrT = false;
    public TMP_Text DayText;
    [Header("Pos")]
    public GameObject Screen;
    public Vector3 ScreenPo;
    public Quaternion ScreenRo;
    [Header("Script")]
    public ClickableObject Co;
    public Camerascript Cs;
    public ActionChecker Ac;
    [Header("VDOData")]
    public VDOData[] allVDOData;
    private VDOData selectedVDOData;
    private VDOData selectedChecklistData;
    public Transform uiCanvasParent;
    public static DayManager Instance;
    public int Day = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    void Start()
    {
        ScreenPo = Screen.transform.position;
        ScreenRo = Screen.transform.rotation;

        if (allVDOData.Length == 0)
        {
            Debug.LogWarning("ยังไม่มี VDOData ให้สุ่มเลย!");
            return;
        }
        ShowRandomVDOAndChecklist();
        Ac.DM = this;
    }
    public void ShowRandomVDOAndChecklist()
    {
        if (currentVideoObj != null)
        {
            Destroy(currentVideoObj);
            currentVideoObj = null;
        }

        if (currentChecklistUI != null)
        {
            Destroy(currentChecklistUI);
            currentChecklistUI = null;
        }

        if (allVDOData.Length == 0 || uiCanvasParent == null) return;

        selectedVDOData = allVDOData[Random.Range(0, allVDOData.Length)];
        selectedChecklistData = selectedVDOData;
        switch (selectedVDOData.name){ 
            case "Mr.P":
                isMrP = true;
                isMrT = false;
                break;
            case "Mr.T":
                isMrP = false;
                isMrT = true;
                break;
            default:
                isMrP = false;
                isMrT = false;
                break;
        }




        if (selectedVDOData.videoPrefab != null)
        {
            Debug.Log("Spawn Now");
            currentVideoObj = Instantiate(selectedVDOData.videoPrefab, ScreenPo , ScreenRo);
            Co = currentVideoObj.GetComponent<ClickableObject>();

            Camerascript camScript = EventSystem.GetComponentInChildren<Camerascript>();
            if (camScript != null)
            {
                Co.CS = camScript;
                Debug.Log("ดึง Camerascript สำเร็จ");
            }else Debug.Log("ดึง Camerascript ไมได้");
        }
        if (selectedChecklistData.ChecklistPrefab != null)
        {
            currentChecklistUI = Instantiate(selectedChecklistData.ChecklistPrefab, uiCanvasParent);
            Cs.ChecklistToggle = currentChecklistUI;
            currentChecklistUI.SetActive(false); 

            Button[] buttons = currentChecklistUI.GetComponentsInChildren<Button>(true);

            foreach (Button btn in buttons)
            {
                if (btn.name == "ignore")
                {
                    Ac.ignoreButton = btn;

                }
                else if (btn.name == "Report")
                {
                    Ac.reportButton = btn;
                    Ac.reportButton.gameObject.SetActive(false);
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

            Toggle[] toggles = currentChecklistUI.GetComponentsInChildren<Toggle>(true);
            Ac.actionToggles = new List<Toggle>(toggles);

            foreach (Toggle toggle in Ac.actionToggles)
            {
                toggle.onValueChanged.AddListener(delegate { Ac.SendMessage("CheckActions"); });
            }
        }

        Debug.Log("Description: " + selectedVDOData.description);
    }

}
