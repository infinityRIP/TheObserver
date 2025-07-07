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
            Debug.LogWarning("�ѧ����� VDOData ����������!");
            return;
        }
        ShowRandomVDOAndChecklist();

    }

    public void ShowRandomVDOAndChecklist()
    {
        if (allVDOData.Length == 0 || uiCanvasParent == null) return;

        // ���� ScriptableObject �������
        selectedVDOData = allVDOData[Random.Range(0, allVDOData.Length)];
        selectedChecklistData = selectedVDOData; // �������ǡѹ

        Debug.Log("������: " + selectedVDOData.name);

        // Instantiate �Դ���
        if (selectedVDOData.videoPrefab != null)
        {
            Instantiate(selectedVDOData.videoPrefab, transform);
        }

        // Instantiate Checklist UI
        if (selectedChecklistData.ChecklistPrefab != null)
        {
            GameObject checklistUI = Instantiate(selectedChecklistData.ChecklistPrefab, uiCanvasParent);
            checklistUI.SetActive(false); // ��͹����͹ �����Դ��µ����ͧ���
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
                Debug.LogWarning("��辺���� Ignore � ChecklistPrefab");
            else
                Ac.ignoreButton.onClick.AddListener(() => Ac.ReportActions());
            if (Ac.reportButton == null)
                Debug.LogWarning("��辺���� Report � ChecklistPrefab");
            else
                Ac.reportButton.onClick.AddListener(() => Ac.ReportActions());

            Toggle[] toggles = checklistUI.GetComponentsInChildren<Toggle>(true);
            Ac.actionToggles = new List<Toggle>(toggles);

            //���� Listener ��� Toggle �ء��� (����͹� Start())
            foreach (Toggle toggle in Ac.actionToggles)
            {
                toggle.onValueChanged.AddListener(delegate { Ac.SendMessage("CheckActions"); });
            }
        }

        Debug.Log("Description: " + selectedVDOData.description);
    }

}
