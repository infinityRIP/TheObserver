using UnityEngine;

public class Blinkingcode : MonoBehaviour
{
    public Renderer rend;
    public float pulseSpeed = 2f; // How fast it pulses
    public bool calling;

    void Start()
    {
        calling = false;
        // Make sure emission is enabled
        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (calling == true)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);

            // Lerp from black to green based on t
            Color emissionColor = Color.Lerp(Color.black, Color.green, t);

            // Apply to emission
            rend.material.SetColor("_EmissionColor", emissionColor);
        }
        else {             // If not calling, set emission to black
            rend.material.SetColor("_EmissionColor", Color.black);
        }


    }
    public void OnMouseDown()
    {
        if (calling == false)
        {
            calling = true;
        }
        else
        {
            calling = false;
        }
    }

}
