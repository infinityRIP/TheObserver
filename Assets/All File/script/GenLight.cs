using System.Dynamic;
using UnityEngine;

public class GenLight : MonoBehaviour
{
    public Renderer red;
    public bool gening;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gening = false;
        red.material.EnableKeyword("_EMISSION");

    }

    // Update is called once per frame
    void Update()
    {
        if (gening == true)
        {
            red.material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            red.material.SetColor("_EmissionColor", Color.red);
        }

    }
    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gening = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gening = false;
        }



    }
    public void OnMouseExit()
    {
        gening = false;
    }
}
