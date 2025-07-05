using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Camerascript CS;
    void Start()
    {
        
    }
    public void OnMouseDown()
    {
        if (CS != null)
        {
            CS.Zoom();

        }
    }
}
