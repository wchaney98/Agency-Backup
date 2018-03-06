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

    public static ACardBehavior CreateAgentCard(Agent agent)
    {
        ACardBehavior card = CreateCard(CardType.Agent);
        card.SetupCard(agent.Title, agent.Description);
        return card;
    }

    public static ACardBehavior CreateContractCard(Contract contract)
    {
        ACardBehavior card = CreateCard(CardType.Contract);
        card.SetupCard(contract.Title, contract.Description);
        return card;
    }

    private static ACardBehavior CreateCard(CardType type)
    {
        GameObject GO = GameObject.Instantiate(type == CardType.Agent ? agentCardPrefab : contractCardPrefab,
            Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        ACardBehavior card = GO.GetComponent<ACardBehavior>();
        return card;
    }
}