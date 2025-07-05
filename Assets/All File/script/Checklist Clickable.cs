using UnityEngine;

public class ChecklistClickable : MonoBehaviour
{
    public Camerascript CS;
    void Start()
    {

    }
    public void OnMouseDown()
    {
        if (CS != null)
        {
            Debug.Log("Check list Open");
            CS.Checklist();
        }
    }
}
