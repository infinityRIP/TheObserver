using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DayManager : MonoBehaviour
{
    [Header("Object")]
    public GameObject EventSystem;
    private GameObject currentVideoObj;
    private GameObject currentChecklistUI;
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
        ScreenPo = Screen.transform.position;
        ScreenRo = Screen.transform.rotation;

        if (allVDOData.Length == 0)
        {
            Debug.LogWarning("�ѧ����� VDOData ����������!");
            return;
        }
        ShowRandomVDOAndChecklist();

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

        Debug.Log("������: " + selectedVDOData.name);

        if (selectedVDOData.videoPrefab != null)
        {
            Debug.Log("Spawn Now");
            currentVideoObj = Instantiate(selectedVDOData.videoPrefab, ScreenPo , ScreenRo);
            Co = currentVideoObj.GetComponent<ClickableObject>();

            Camerascript camScript = EventSystem.GetComponentInChildren<Camerascript>();
            if (camScript != null)
            {
                Co.CS = camScript;
                Debug.Log("�֧ Camerascript �����");
            }else Debug.Log("�֧ Camerascript ����");
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
                Debug.LogWarning("��辺���� Ignore � ChecklistPrefab");
            else
                Ac.ignoreButton.onClick.AddListener(() => Ac.ReportActions());
            if (Ac.reportButton == null)
                Debug.LogWarning("��辺���� Report � ChecklistPrefab");
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
