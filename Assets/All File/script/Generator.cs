using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public RandomEvent RnEv;
    public Power P;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                if (RnEv.isPowerFast == false)
                {

                    P.isGen = true;
                }
            }
            else
            {
                P.isGen = false;
            }
        }
        else
        {
            P.isGen = false;
        }
    }

    
}

