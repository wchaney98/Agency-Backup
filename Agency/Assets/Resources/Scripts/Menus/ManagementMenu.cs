using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagementMenu : MonoBehaviour
{
    public Text MoneyText;
    public Text RepText;

    private GameObject agentCardPrefab;
    private GameObject contractCardPrefab;

    private void Awake()
    {
        Debug.Log(PlayerData.Instance.Agents.Count);
        if (PlayerData.Instance.Agents.Count == 0)
        {
            SceneManager.LoadScene("GameEndScene");
        }

        agentCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/AgentCard");
        contractCardPrefab = Resources.Load<GameObject>("Prefabs/Menus/ContractCard");
    }

    private void Start()
    {
        MoneyText.text = "MONEY: " + PlayerData.Instance.Money;
        RepText.text = "REPUTATION: " + PlayerData.Instance.Reputation;

        foreach (Agent agent in PlayerData.Instance.Agents)
        {
            AgentCardBehavior c = CardCreator.CreateAgentCard(agent);
        }
        CardCreator.Reset();

        PlayerData.Instance.Contracts.Clear();
        for (int i = 0; i < 5; i++)
        {
            ContractModifiers cm = new ContractModifiers();
            cm.MapSize = Random.Range(0.8f, 2f);
            TileType[,] level = LevelGenerator.Generate(cm);
            Contract c = new Contract("Contract Case:", new StringBuilder("#" + UnityEngine.Random.Range(0, 300).ToString()), level, new List<WinConditions>() { WinConditions.Clear }, Random.Range(50, 250), Random.Range(50, 250));
            PlayerData.Instance.Contracts.Add(c);
        }
        foreach (Contract contract in PlayerData.Instance.Contracts)
        {
            ContractCardBehavior c = CardCreator.CreateContractCard(contract);
        }
        CardCreator.Reset();
    }
}