using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagementMenu : MonoBehaviour
{
    private GameObject agentCardPrefab;
    private GameObject contractCardPrefab;

    private void Awake()
    {
        agentCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/AgentCard");
        contractCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/ContractCard");
    }

    private void Start()
    {
        foreach (Agent agent in PlayerData.Instance.Agents)
        {
            AgentCardBehavior c = CardCreator.CreateAgentCard(agent);
            Debug.Log("a: " + c.Agent.AgentType);
        }
        CardCreator.Reset();
        foreach (Contract contract in PlayerData.Instance.Contracts)
        {
            ContractCardBehavior c = CardCreator.CreateContractCard(contract);
            Debug.Log("c: " + c.Contract.Tiles);
        }
        CardCreator.Reset();
    }
}