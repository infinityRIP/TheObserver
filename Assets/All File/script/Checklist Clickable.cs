using UnityEngine;

public class ChecklistClickable : MonoBehaviour
{
    public Camerascript CS;

    public void OnMouseDown()
    {
        if (CS != null)
        {
            CS.Checklist();
        }
    }
}
