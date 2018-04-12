using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator
{
    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public static TileType[,] Generate(ContractModifiers mods)
    {
        // Create array
        int width = (int)(ContractModifiers.DefaultMapSize * mods.MapSize * 2) + UnityEngine.Random.Range(1, 10);
        int height = 11;// (int)(ContractModifiers.DefaultMapSize * mods.MapSize);
        TileType[,] level = new TileType[width + 1 , height + 1];

        for (int i = 0; i != level.GetLength(0); i++)
        {
            for (int j = 0; j != level.GetLength(1); j++)
            {
                level[i, j] = TileType.Floor;
            }
        }

        for (int i = 0; i < level.GetLength(0); i++)
        {
            level[i, 0] = TileType.Wall;
            level[i, level.GetLength(1) - 1] = TileType.Wall;
        }
        for (int i = 0; i < level.GetLength(1); i++)
        {
            level[0, i] = TileType.Wall;
            level[level.GetLength(0) - 1, i] = TileType.Wall;
        }

        int numRooms = UnityEngine.Random.Range(3, 7);

        Vector2 currRoomBotLeft = new Vector2(0, 0);

        for (int i = 0; i < numRooms; i++)
        {
            BuildRoom(level, ref currRoomBotLeft);
        }

        level[1, 1] = TileType.PlayerSpawn;

        // Create halls (of width), store chunks of room
        

        // Split chunks of room into half by chance


        // Connect rooms with doors


        // Place enemies


        return level;
    }

    private static void BuildRoom(TileType [,] level, ref Vector2 botLeft)
    {
        int roomType = UnityEngine.Random.Range(0, 5);
        // 0-1 small room
        // 2 big room
        // 3-4 hall

        int size = UnityEngine.Random.Range(7, 11);
        int x = (int)botLeft.x;
        int y = (int)botLeft.y;

        if (x + size >= level.GetLength(0) || y + size >= level.GetLength(1))
            return;

        for (int i = x; i < x + size + 10; i++)
        {
            level[i, y] = TileType.Wall;
            level[i, y + size] = TileType.Wall;
        }
        for (int i = y; i < y + size; i++)
        {
            if (level[x, i] != TileType.Door)
                level[x, i] = TileType.Wall;
            level[x + size, i] = TileType.Wall;
        }

        if (botLeft != Vector2.zero)
        {
            Array values = Enum.GetValues(typeof(TileType));
            TileType randEnemy = (TileType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            while (randEnemy != TileType.BasicEnemySpawn && randEnemy != TileType.BasicRobotSpawn && randEnemy != TileType.MeleeEnemy && randEnemy != TileType.TurretSpawn)
            {
                randEnemy = (TileType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            }
            level[x + size / 2 + 2, y + size / 2] = randEnemy;
        }

        level[x + size / 2 + (UnityEngine.Random.Range(0, 1f) >= 0.5f ? -1 : 1), y + size / 2] = TileType.Cover;

        level[x + size, y + size / 2] = TileType.Door;
        botLeft.x += size;
    }
}
