using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotButtonBehavior : MonoBehaviour
{
    public int SlotNumber;
    
    private Text text;
    private string slotName;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        slotName = text.text;

        // Check data file
        if (!File.Exists(Application.dataPath + "/StreamingAssets/SaveData/" + text.text))
        {
            text.text = "empty";
        }
    }

    public void OnClick()
    {
        PersistentData.Instance.CurrentSaveSlot = SlotNumber;
        if (File.Exists(Application.dataPath + "/StreamingAssets/SaveData/" + slotName))
        {
            PlayerData.Instance.Load();
        }
        else
        {
            // Starting agents
            Agent agent = new Agent
            {
                Title = "Agent",
                Description = new StringBuilder("The Breacher"),
                AgentType = AgentType.Breacher,
                MoveSpeed = 7f,
                PrimaryCooldown = 0.1f,
                SpecialCooldown = 1f
            };

            Agent agent1 = new Agent
            {
                Title = "Agent",
                Description = new StringBuilder("Standard Issue"),
                AgentType = AgentType.Standard,
                PrimaryCooldown = 0.5f,
                SpecialCooldown = 1f
            };

            Agent agent2 = new Agent
            {
                Title = "Agent",
                Description = new StringBuilder("Demolitions"),
                AgentType = AgentType.Joker,
                PrimaryCooldown = 0.5f,
                SpecialCooldown = 1f,
                Level = 2
            };

            PlayerData.Instance.Agents.Add(agent);
            PlayerData.Instance.Agents.Add(agent1);
            PlayerData.Instance.Agents.Add(agent2);

            PlayerData.Instance.Save();
        }
        SceneManager.LoadScene("ManagementScene");
    }
}