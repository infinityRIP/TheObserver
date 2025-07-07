using UnityEngine;

[CreateAssetMenu(fileName = "VideoData", menuName = "Set/Video & Checklist Set")]
public class VDOData : ScriptableObject
{
    public GameObject videoPrefab;
    public GameObject ChecklistPrefab;
    public string description;
}
