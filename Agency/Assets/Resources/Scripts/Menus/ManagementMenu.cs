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
            CardCreator.CreateAgentCard(agent);
        }
        CardCreator.Reset();
        foreach (Contract contract in PlayerData.Instance.Contracts)
        {
            CardCreator.CreateContractCard(contract);
        }
        CardCreator.Reset();
    }
}