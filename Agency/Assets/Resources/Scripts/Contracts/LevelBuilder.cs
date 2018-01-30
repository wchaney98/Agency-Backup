using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Cover,
    PlayerSpawn
}

public static class LevelBuilder
{
    static GameObject floorPrefab;
    static GameObject wallPrefab;
    static GameObject coverPrefab;

    static Dictionary<TileType, GameObject> wallTypeToPrefab = new Dictionary<TileType, GameObject>();

    public static bool Inititialize()
    {
        floorPrefab = Resources.Load<GameObject>("Prefabs/World/floor_grey");
        wallPrefab = Resources.Load<GameObject>("Prefabs/World/wall_grey");
        coverPrefab = Resources.Load<GameObject>("Prefabs/World/cover_grey");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Characters/Player");

        if (floorPrefab == null || wallPrefab == null || coverPrefab == null || playerPrefab == null)
        {
            return false;
        }

        wallTypeToPrefab.Add(TileType.Floor, floorPrefab);
        wallTypeToPrefab.Add(TileType.Wall, wallPrefab);
        wallTypeToPrefab.Add(TileType.Cover, coverPrefab);
        wallTypeToPrefab.Add(TileType.PlayerSpawn, playerPrefab);

        return true;
    }

    public static void BuildLevel(TileType[,] tiles)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                GameObject.Instantiate(wallTypeToPrefab[tiles[i, j]], new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity);
                if (tiles[i, j] == TileType.PlayerSpawn)
                    GameObject.Instantiate(floorPrefab, new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity);
            }
        }
    }
}
