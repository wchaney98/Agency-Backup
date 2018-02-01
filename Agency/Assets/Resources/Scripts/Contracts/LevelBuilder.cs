﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Cover,
    CoverArea,
    PlayerSpawn,
    BasicEnemySpawn
}

public static class LevelBuilder
{
    static GameObject floorPrefab;
    static GameObject wallPrefab;
    static GameObject coverPrefab;
    static GameObject coverAreaPrefab;
    static GameObject playerPrefab;
    static GameObject basicEnemyPrefab;

    static GameObject parent;

    static Dictionary<TileType, GameObject> wallTypeToPrefab = new Dictionary<TileType, GameObject>();

    public static bool Inititialize()
    {
        floorPrefab = Resources.Load<GameObject>("Prefabs/World/floor_grey");
        wallPrefab = Resources.Load<GameObject>("Prefabs/World/wall_grey");
        coverPrefab = Resources.Load<GameObject>("Prefabs/World/cover_grey");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Characters/PlayerContainer");
        basicEnemyPrefab = Resources.Load<GameObject>("Prefabs/Characters/BasicEnemy");
        coverAreaPrefab = Resources.Load<GameObject>("Prefabs/World/cover_area");

        if (floorPrefab == null || wallPrefab == null || coverPrefab == null || playerPrefab == null || basicEnemyPrefab == null || coverAreaPrefab == null)
        {
            Debug.Log("Levelbuilder failed to init");
            return false;
        }

        wallTypeToPrefab.Add(TileType.Floor, floorPrefab);
        wallTypeToPrefab.Add(TileType.Wall, wallPrefab);
        wallTypeToPrefab.Add(TileType.Cover, coverPrefab);
        wallTypeToPrefab.Add(TileType.PlayerSpawn, playerPrefab);
        wallTypeToPrefab.Add(TileType.BasicEnemySpawn, basicEnemyPrefab);
        wallTypeToPrefab.Add(TileType.CoverArea, coverAreaPrefab);

        parent = GameObject.Instantiate<GameObject>(new GameObject());

        return true;
    }

    public static void BuildLevel(TileType[,] tiles)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                GameObject.Instantiate(wallTypeToPrefab[tiles[i, j]], new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                if (tiles[i, j] == TileType.PlayerSpawn || tiles[i, j] == TileType.BasicEnemySpawn)
                    GameObject.Instantiate(floorPrefab, new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                if (tiles[i, j] == TileType.Cover)
                {
                    if (tiles[i + 1, j] != TileType.Wall && tiles[i + 1, j] != TileType.Cover && tiles[i + 1, j] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i + 1) * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                    if (tiles[i - 1, j] != TileType.Wall && tiles[i - 1, j] != TileType.Cover && tiles[i - 1, j] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i - 1) * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                    if (tiles[i, j + 1] != TileType.Wall && tiles[i, j + 1] != TileType.Cover && tiles[i, j + 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3(i * 0.64f, (j + 1) * 0.64f), Quaternion.identity, parent.transform);

                    if (tiles[i, j - 1] != TileType.Wall && tiles[i, j - 1] != TileType.Cover && tiles[i, j - 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3(i * 0.64f, (j - 1) * 0.64f), Quaternion.identity, parent.transform);
                }
            }
        }
    }
}
