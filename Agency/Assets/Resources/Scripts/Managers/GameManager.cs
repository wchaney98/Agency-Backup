using System;
using System.Collections.Generic;
using UnityEngine;

class GameManager : MonoBehaviour
{
    private void Start()
    {
        TileType[,] testLevel = new TileType[16,8];

        for (int i = 0; i != testLevel.GetLength(0); i++)
        {
            for (int j = 0; j != testLevel.GetLength(1); j++)
            {
                testLevel[i, j] = TileType.Floor;
            }
        }
        testLevel[2, 2] = TileType.Wall;
        testLevel[2, 3] = TileType.Wall;

        testLevel[5, 6] = TileType.Cover;
        testLevel[6, 6] = TileType.Cover;
        testLevel[4, 6] = TileType.Cover;

        testLevel[8, 4] = TileType.PlayerSpawn;

        LevelBuilder.Inititialize();
        LevelBuilder.BuildLevel(testLevel);
    }
}
