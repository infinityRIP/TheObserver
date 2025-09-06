using UnityEngine;

[CreateAssetMenu(fileName = "VideoData", menuName = "Set/Video & Checklist Set")]
public class VDOData : ScriptableObject
{
    public GameObject videoPrefab;
    public GameObject ChecklistPrefab;
    public string description;
    public int ID;
    public bool Check1;
    public bool Check2;
    public bool Check3;
    public bool Check4;
    public bool MatchesToggles(bool[] toggles)
    {
        return toggles.Length >= 4 &&
               toggles[0] == Check1 &&
               toggles[1] == Check2 &&
               toggles[2] == Check3 &&
               toggles[3] == Check4;
    }
}
