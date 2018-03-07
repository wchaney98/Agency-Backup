using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotBehavior : MonoBehaviour
{
    public bool CardLockedIn = false;
    public ACardBehavior LockedCard { get; private set; }
    
    Image image;
    Text text;

    string textHolder;

    public void LockInCard(ACardBehavior card)
    {
        CardLockedIn = true;
        LockedCard = card;
        
        Color temp = image.color;
        temp.a = 0f;
        image.color = temp;

        text.text = "";
    }

    public void UnlockCard(ACardBehavior caller)
    {
        if (CardLockedIn && LockedCard == caller && LockedCard != null)
        {
            CardLockedIn = false;
            LockedCard = null;
            
            Color temp = image.color;
            temp.a = .5f;
            image.color = temp;

            text.text = textHolder;
        }
    }
    
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        textHolder = text.text;
    }
}
