using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType
{
    Agent,
    Contract
}

public static class CardCreator
{
    private static GameObject agentCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/AgentCard");
    private static GameObject contractCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/ContractCard");

    private static int currentY = 0;

    public static void Reset()
    {
        currentY = 0;
    }
    
    public static AgentCardBehavior CreateAgentCard(Agent agent)
    {
        ACardBehavior card = CreateCard(CardType.Agent);
        ((AgentCardBehavior)card).SetupCard(agent.Title, agent.Description);
        return (AgentCardBehavior)card;
    }

    public static ContractCardBehavior CreateContractCard(Contract contract)
    {
        ACardBehavior card = CreateCard(CardType.Contract);
        ((ContractCardBehavior)card).SetupCard(contract.Title, contract.Description);
        return (ContractCardBehavior)card;
    }

    private static ACardBehavior CreateCard(CardType type)
    {
        GameObject GO = GameObject.Instantiate(type == CardType.Agent ? agentCardPrefab : contractCardPrefab,
            new Vector3(0f, currentY, 0f), Quaternion.identity, GameObject.Find("Canvas").transform);
        Vector3 temp = GO.GetComponent<RectTransform>().anchoredPosition;
        temp.y += currentY;
        temp.z = 0;
        GO.GetComponent<RectTransform>().anchoredPosition = temp;
        currentY += 109;
        ACardBehavior card = GO.GetComponent<ACardBehavior>();
        return card;
    }
}