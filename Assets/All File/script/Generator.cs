using UnityEngine;

public class Generator : MonoBehaviour
{
    public RandomEvent RnEv;
    public Power P;
    public AudioSource GenSound;

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
                    if (!GenSound.isPlaying)
                    {
                        Debug.LogWarning("genSound is playing");
                        GenSound.Play();
                    }
                }
            }
            else
            {
                P.isGen = false;
                GenSound.Stop();
            }
        }
        else
        {
            P.isGen = false;
            GenSound.Stop();
        }
    }
}
