using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonBehavior : MonoBehaviour
{
    public GameObject AgentSlot;
    public GameObject ContractSlot;

    CardSlotBehavior agentSlotBehavior;
    CardSlotBehavior contractSlotBehavior;

    Button button;
    Image image;

    void Start()
    {
        agentSlotBehavior = AgentSlot.GetComponent<CardSlotBehavior>();
        contractSlotBehavior = ContractSlot.GetComponent<CardSlotBehavior>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (agentSlotBehavior.CardLockedIn && contractSlotBehavior.CardLockedIn)
        {
            button.interactable = true;
            Color temp = image.color;
            temp.a = 0.5f;
            image.color = temp;
            GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            button.interactable = false;
            Color temp = image.color;
            temp.a = 1f;
            image.color = temp;
            GetComponentInChildren<Text>().color = Color.white;
        }
    }

    public void StartGame()
    {
        // TODO: use actual contract stuff
        SceneManager.LoadScene("MainGame");
    }
}
