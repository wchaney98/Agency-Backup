using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinker : MonoBehaviour
{
    public float Interval = 1f;
    public string PhaseOne = "";
    public string PhaseTwo = "";

    Text text;
    float timer = 0f;
    int phase = 1;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Interval)
        {
            if (phase == 1)
            {
                text.text = PhaseOne;
                phase = 2;
            }
            else
            {
                text.text = PhaseTwo;
                phase = 1;
            }
            timer = 0f;
        }
    }
}
