using UnityEngine;

public class checklist : MonoBehaviour
{
    public GameObject checklistobj;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        checklistobj.SetActive(true);

    }
}
