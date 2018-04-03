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
        int width = (int)(ContractModifiers.DefaultMapSize * mods.MapSize) + Random.Range(1, 10);
        int height = (int)(ContractModifiers.DefaultMapSize * mods.MapSize);
        TileType[,] level = new TileType[width, height];

        for (int i = 0; i != level.GetLength(0); i++)
        {
            for (int j = 0; j != level.GetLength(1); j++)
            {
                level[i, j] = TileType.Floor;
            }
        }

        // Create halls (of width), store chunks of room
        int hallWidth = 3;

        // Start pt for player
        Vector2 startingPoint = Random.Range(0, 2) == 1 ? new Vector2(width / 2 + Random.Range(-10, 10), 0) : new Vector2(0, height / 2 + Random.Range(-10, 10));

        Direction dir = Direction.UP;
        bool doneCarving = false;
        Vector2 travel = new Vector2(startingPoint.x, startingPoint.y);

        // Carve until another edge met
        while (!doneCarving)
        {
            switch (dir)
            {
                case Direction.UP:
                    // Check bounds
                    // Place halls of hallWidth
                    // Move travel up 1
                    break;
            }
        }

        // Split chunks of room into half by chance


        // Connect rooms with doors


        // Place enemies


        return level;
    }
}
