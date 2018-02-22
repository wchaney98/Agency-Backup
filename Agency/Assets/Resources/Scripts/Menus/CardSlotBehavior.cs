using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotBehavior : MonoBehaviour
{
    public bool CardLockedIn = false;

    Image image;
    Text text;

    string textHolder;

    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        textHolder = text.text;
    }

    void Update()
    {
        if (CardLockedIn)
        {
            Color temp = image.color;
            temp.a = 0f;
            image.color = temp;

            text.text = "";
        }
        else
        {
            Color temp = image.color;
            temp.a = .5f;
            image.color = temp;

            text.text = textHolder;
        }
    }
}
