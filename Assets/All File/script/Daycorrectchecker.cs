using UnityEngine;

public class Daycorrectchecker : MonoBehaviour
{
    public bool isCorrectDay1 = false;
    public bool isCorrectDay2 = false;
    public bool isCorrectDay3 = false;
    public bool isCorrectDay4 = false;
    public bool isCorrectDay5 = false;
    public static Daycorrectchecker correct;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (correct == null)
        {
            correct = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

