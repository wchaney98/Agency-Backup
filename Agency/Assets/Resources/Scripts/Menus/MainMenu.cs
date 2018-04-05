using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        TileType[,] testLevel = new TileType[48, 8];

        for (int i = 0; i != testLevel.GetLength(0); i++)
        {
            for (int j = 0; j != testLevel.GetLength(1); j++)
            {
                testLevel[i, j] = TileType.Floor;
            }
        }

        for (int i = 0; i != testLevel.GetLength(0); i++)
        {
            testLevel[i, 0] = TileType.Wall;
            testLevel[i, 7] = TileType.Wall;
        }

        for (int i = 0; i != testLevel.GetLength(1); i++)
        {
            testLevel[0, i] = TileType.Wall;
            testLevel[47, i] = TileType.Wall;
        }

        for (int i = 0; i != testLevel.GetLength(1); i++)
        {
            if (i != 3 && i != 4)
                testLevel[23, i] = TileType.Wall;
            else
                testLevel[23, i] = TileType.Door;
        }

        testLevel[3, 2] = TileType.Cover;
        testLevel[3, 3] = TileType.Cover;
        testLevel[3, 5] = TileType.Cover;
        testLevel[9, 1] = TileType.Cover;
        testLevel[9, 6] = TileType.Cover;
        testLevel[9, 5] = TileType.Cover;
        testLevel[13, 3] = TileType.Cover;
        testLevel[13, 4] = TileType.Cover;

        testLevel[43, 2] = TileType.Cover;
        testLevel[43, 3] = TileType.Cover;
        testLevel[42, 5] = TileType.Cover;
        testLevel[37, 1] = TileType.Cover;
        testLevel[36, 6] = TileType.Cover;
        testLevel[35, 5] = TileType.Cover;
        testLevel[33, 3] = TileType.Cover;
        testLevel[33, 4] = TileType.Cover;

        testLevel[14, 3] = TileType.PlayerSpawn;
        testLevel[2, 2] = TileType.BasicRobotSpawn;
        testLevel[2, 4] = TileType.BasicEnemySpawn;

        testLevel[46, 2] = TileType.BasicRobotSpawn;
        testLevel[46, 4] = TileType.BasicEnemySpawn;
        testLevel[41, 6] = TileType.BasicEnemySpawn;


        StringBuilder sb = new StringBuilder();
        sb.Append("dev");
        Contract testContract1 = new Contract("ProtoLvl", sb, testLevel,
            new List<WinConditions>() {WinConditions.Clear}, 10, 10);
        Contract testContract2 = new Contract("VertSlice", sb, LevelParser.TextToTiles("level1"),
            new List<WinConditions>() {WinConditions.Clear}, 20, 20);
        
        //PlayerData.Instance.Contracts.Add(testContract1);
        //PlayerData.Instance.Contracts.Add(testContract2);

        Agent agent = new Agent
        {
            Title = "Breacher",
            Description = sb,
            AgentType = AgentType.Breacher,
            MoveSpeed = 7f,
            PrimaryCooldown = 0.1f,
            SpecialCooldown = 1f
        };
        
        Agent agent1 = new Agent
        {
            Title = "Standard",
            Description = sb,
            AgentType = AgentType.Standard,
            PrimaryCooldown = 0.5f,
            SpecialCooldown = 1f
        };

        Agent agent2 = new Agent
        {
            Title = "Demolition",
            Description = sb,
            AgentType = AgentType.Joker,
            PrimaryCooldown = 0.5f,
            SpecialCooldown = 1f,
            Level = 2
        };

        PlayerData.Instance.Agents.Add(agent);
        PlayerData.Instance.Agents.Add(agent1);
        PlayerData.Instance.Agents.Add(agent2);


        PersistentData.Instance.CurrentSaveSlot = 1;
        
        PlayerData.Instance.Save();
    }

    public void Play()
    {
        SceneManager.LoadScene("SaveMenu");
    }

    public void Help()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
