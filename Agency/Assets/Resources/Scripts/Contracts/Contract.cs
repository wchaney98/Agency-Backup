﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum WinConditions
{
    Clear,
    Timer,
    Target
}

public class Contract
{
    public string Title { get; set; }
    public StringBuilder Description { get; set; }
    public TileType[,] Tiles { get; set; }
    public List<WinConditions> WinList;

    public Contract(string title, StringBuilder description, TileType[,] tiles, List<WinConditions> winList)
    {
        Title = title;
        Description = description;
        Tiles = tiles;
        WinList = winList;
    }
}
