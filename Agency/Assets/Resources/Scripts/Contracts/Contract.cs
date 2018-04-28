using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public enum WinConditions
{
    Clear,
    Timer,
    Target
}

[Serializable]
public class Contract
{
    public string Title { get; set; }
    public StringBuilder Description { get; set; }
    public TileType[,] Tiles { get; set; }
    public List<WinConditions> WinList;
    public int MoneyAward;
    public int ReputationAward;
    public int DifficultyScore;

    public Contract(string title, StringBuilder description, TileType[,] tiles, List<WinConditions> winList, int moneyAward, int reputationAward)
    {
        Title = title;
        Description = description;
        Tiles = tiles;
        WinList = winList;
        MoneyAward = moneyAward;
        ReputationAward = reputationAward;
    }
}
