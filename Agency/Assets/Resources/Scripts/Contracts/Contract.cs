using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Contract
{
    public string Title { get; set; }
    public StringBuilder Description { get; set; }
    public TileType[,] Tiles { get; set; }

    public Contract(string title, StringBuilder description, TileType[,] tiles)
    {
        Title = title;
        Description = description;
        Tiles = tiles;
    }
}
