using UnityEngine;
using UnityEngine.UI; // ��䢨ҡ UnityEngine.Ui �� UnityEngine.UI

public class endscripts : MonoBehaviour
{
    public float scrollSpeed = 70f;

    private RectTransform rectTransform; // ��䢨ҡ ReactTransform �� RectTransform

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // ��䢨ҡ ReactTransform �� RectTransform
        // ��Ǩ�ͺ����� RectTransform ���������
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not found on this GameObject. Please ensure this script is attached to a UI element with a RectTransform component.");
            enabled = false; // �Դ script �������� RectTransform
        }
    }

    void Update()
    {
        // �ӹǳ�������͹�᡹ Y
        float yMovement = scrollSpeed * Time.deltaTime;
        // �������˹��᡹ Y ���Ѻ anchoredPosition
        rectTransform.anchoredPosition += new Vector2(0, yMovement); // ��䢡�����ҧ Vector2 ��С�äӹǳ
    }
}