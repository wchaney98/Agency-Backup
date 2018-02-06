﻿using System;
using System.Collections.Generic;
using UnityEngine;

class GameManager : MonoBehaviour
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
        testLevel[2, 2] = TileType.BasicEnemySpawn;
        testLevel[2, 4] = TileType.BasicEnemySpawn;

        testLevel[46, 2] = TileType.BasicEnemySpawn;
        testLevel[46, 4] = TileType.BasicEnemySpawn;
        testLevel[41, 6] = TileType.BasicEnemySpawn;

        LevelBuilder.Inititialize();
        LevelBuilder.BuildLevel(testLevel);
    }
}
