using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator
{
    private static Array tileTypes = Enum.GetValues(typeof(TileType));
    private static int currHighestHeight = 0;
    private static int lastRoomInRowHeight = 0;
    private static bool buildingRight = true;

    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public static TileType[,] Generate(ContractModifiers mods)
    {
        // reset vars
        currHighestHeight = 0;
        lastRoomInRowHeight = 0;
        buildingRight = true;

        // Create array
        bool tallOrWide = UnityEngine.Random.Range(0, 2) == 0;
        int width = (int)(ContractModifiers.DefaultMapSize * mods.MapSize * (tallOrWide ? 1 : 1.5)) + UnityEngine.Random.Range(1, 4);
        int height = (int)(ContractModifiers.DefaultMapSize * mods.MapSize * (tallOrWide ? 2 : 1.5)) + UnityEngine.Random.Range(1, 4);// (int)(ContractModifiers.DefaultMapSize * mods.MapSize);
        TileType[,] level = new TileType[width + 1, height + 1];

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

    private static void BuildRoom(TileType[,] level, ref Vector2 botLeft)
    {
        int roomType = UnityEngine.Random.Range(0, 5);
        // 0-1 small room
        // 2 big room
        // 3-4 hall (3 vertical, 4 horizontal)

        int size = UnityEngine.Random.Range(4, 8);

        //BuildSmallRoom(level, size, ref botLeft);

        //return;

        if (roomType <= 1)
            BuildSmallRoom(level, size, ref botLeft);
        if (roomType == 2)
            BuildBigRoom(level, size, ref botLeft);
        if (roomType == 3)
            BuildHall(level, size, true, ref botLeft);
        else
            BuildHall(level, size, false, ref botLeft);
    }

    private static void BuildSmallRoom(TileType[,] level, int size, ref Vector2 botLeft)
    {
        int x = (int)botLeft.x;
        int y = (int)botLeft.y;

        bool buildDoorUp = false;

        if (x + size >= level.GetLength(0) - 1)
        {
            buildDoorUp = true;
            while (x + size >= level.GetLength(0))
                size--;
        }

        if (y + size > level.GetLength(1))
            return;

        if (y + size > currHighestHeight)
            currHighestHeight = y + size;

        for (int i = x; i < x + size; i++)
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
            SpawnRandomEnemy(level, new Vector2Int
                    (x + size / 2 + UnityEngine.Random.Range(1, size / 3), y + size / 2 + UnityEngine.Random.Range(1, size / 3)));
        }

        level[x + size / 2 + (UnityEngine.Random.Range(0, 1f) >= 0.5f ? -1 : 1), y + size / 2] = TileType.Cover;

        if (buildDoorUp)
        {
            level[x + size / 2, y + size] = TileType.Door;
            botLeft = new Vector2(0, currHighestHeight);
        }
        else
        {
            level[x + size, y + size / 2] = TileType.Door;
            botLeft.x += size;
        }

        // build door down
        if (botLeft.x == 0 && botLeft.y > 0)
        {
            level[x + size / 2, y] = TileType.Door;
        }
    }

    private static void BuildHall(TileType[,] level, int size, bool vertical, ref Vector2 botLeft)
    {
        int x = (int)botLeft.x;
        int y = (int)botLeft.y;

        int sizeX = size + (vertical ? 0 : size);
        int sizeY = size + (vertical ? size : 0);

        bool buildDoorUp = false;

        if (x + sizeX >= level.GetLength(0) - 1)
        {
            buildDoorUp = true;
            while (x + sizeX >= level.GetLength(0))
                sizeX--;
        }

        if (y + sizeY > level.GetLength(1))
            return;

        if (y + sizeY > currHighestHeight)
            currHighestHeight = y + sizeY;

        for (int i = x; i < x + sizeX; i++)
        {
            level[i, y] = TileType.Wall;
            level[i, y + sizeY] = TileType.Wall;
        }
        for (int i = y; i < y + sizeY; i++)
        {
            if (level[x, i] != TileType.Door)
                level[x, i] = TileType.Wall;
            level[x + sizeX, i] = TileType.Wall;
        }

        if (botLeft != Vector2.zero)
        {
            SpawnRandomEnemy(level, new Vector2Int
                    (x + sizeX / 2 + UnityEngine.Random.Range(1, sizeX / 3), y + sizeY / 2 + UnityEngine.Random.Range(1, sizeY / 3)));
        }

        level[x + sizeX / 2 + (UnityEngine.Random.Range(0, 1f) >= 0.5f ? -1 : 1), y + sizeY / 2] = TileType.Cover;

        if (buildDoorUp)
        {
            level[x + sizeX / 2, y + sizeY] = TileType.Door;
            botLeft = new Vector2(0, currHighestHeight);
        }
        else
        {
            level[x + sizeX, y + sizeY / 3] = TileType.Door;
            botLeft.x += sizeX;
        }

        // build door down
        if (botLeft.x == 0 && botLeft.y > 0)
        {
            level[x + size / 2, y] = TileType.Door;
        }
    }

    private static void BuildBigRoom(TileType[,] level, int size, ref Vector2 botLeft)
    {
        size *= 2;

        int x = (int)botLeft.x;
        int y = (int)botLeft.y;

        bool buildDoorUp = false;

        // build door down
        if (botLeft.x == 0 && botLeft.y > 0)
        {
            level[x + size / 2, y] = TileType.Door;
        }

        if (x + size >= level.GetLength(0) - 1)
        {
            buildDoorUp = true;
            buildingRight = !buildingRight;
            while (x + size >= level.GetLength(0))
                size--;
        }

        if (y + size > level.GetLength(1))
            return;

        if (y + size > currHighestHeight)
            currHighestHeight = y + size;

        for (int i = x; i < x + size; i++)
        {
            if (level[i, y] != TileType.Door)
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
            int numEnemies = UnityEngine.Random.Range(2, 4);
            for (int i = 0; i < numEnemies; i++)
            {
                SpawnRandomEnemy(level, new Vector2Int
                    (x + size / 2 + UnityEngine.Random.Range(1, size / 3), y + size / 2 + UnityEngine.Random.Range(1, size / 3)));
            }
        }

        level[x + size / 2 + (UnityEngine.Random.Range(0, 1f) >= 0.5f ? -1 : 1), y + size / 2] = TileType.Cover;
        level[x + size / 2 + (UnityEngine.Random.Range(0, 1f) >= 0.5f ? -1 : 1), y + size / 2 - 1] = TileType.Cover;

        if (buildDoorUp || !buildingRight)
        {
            level[x + size / 2, y + size] = TileType.Door;
            botLeft = new Vector2(0, currHighestHeight);
            buildingRight = !buildingRight;
        }
        else
        {
            level[x + size, y + size / 3] = TileType.Door;
            botLeft.x += size;
        }

        // build door down
        if (botLeft.x == 0 && botLeft.y > 0)
        {
            level[x + size / 2, y] = TileType.Door;
        }
    }

    private static void SpawnRandomEnemy(TileType[,] level, Vector2Int position)
    {
        TileType randEnemy = (TileType)tileTypes.GetValue(UnityEngine.Random.Range(0, tileTypes.Length));
        while (randEnemy != TileType.BasicEnemySpawn && randEnemy != TileType.BasicRobotSpawn && randEnemy != TileType.MeleeEnemy && randEnemy != TileType.TurretSpawn)
        {
            randEnemy = (TileType)tileTypes.GetValue(UnityEngine.Random.Range(0, tileTypes.Length));
        }
        try
        {
            level[position.x, position.y] = randEnemy;
        }
        catch
        {
            Debug.Log("Enemy was placed in invalid loc");
        }

    }
}
