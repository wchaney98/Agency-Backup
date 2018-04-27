using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Floor = 0,
    Wall = 1,
    Cover = 2,
    CoverArea = 3,
    PlayerSpawn = 4,
    BasicEnemySpawn = 5,
    BasicRobotSpawn = 6,
    Door = 7,
    TurretSpawn = 8,
    MeleeEnemy = 9
}

public static class LevelBuilder
{
    public static GameObject floorPrefab;
    public static GameObject wallPrefab;
    public static GameObject coverPrefab;
    public static GameObject coverAreaPrefab;
    public static GameObject playerPrefab;
    public static GameObject basicEnemyPrefab;
    public static GameObject basicRobotPrefab;
    public static GameObject doorPrefab;
    public static GameObject turretPrefab;
    public static GameObject meleeEnemyPrefab;

    public static GameObject parent;

    static Dictionary<TileType, GameObject> wallTypeToPrefab;

    public static bool Inititialize()
    {
        wallTypeToPrefab = new Dictionary<TileType, GameObject>();

        floorPrefab = Resources.Load<GameObject>("Prefabs/World/floor_grey");
        wallPrefab = Resources.Load<GameObject>("Prefabs/World/wall_grey");
        coverPrefab = Resources.Load<GameObject>("Prefabs/World/cover_grey");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Characters/PlayerContainer");
        basicEnemyPrefab = Resources.Load<GameObject>("Prefabs/Characters/BasicEnemy");
        basicRobotPrefab = Resources.Load<GameObject>("Prefabs/Characters/BasicRobot");
        coverAreaPrefab = Resources.Load<GameObject>("Prefabs/World/cover_area");
        doorPrefab = Resources.Load<GameObject>("Prefabs/World/door");
        turretPrefab = Resources.Load<GameObject>("Prefabs/Characters/BasicTurret");
        meleeEnemyPrefab = Resources.Load<GameObject>("Prefabs/Characters/MeleeEnemy");

        if (floorPrefab == null || wallPrefab == null || coverPrefab == null || playerPrefab == null || basicEnemyPrefab == null || coverAreaPrefab == null
            || basicRobotPrefab == null || doorPrefab == null || turretPrefab == null || meleeEnemyPrefab == null)
        {
            Debug.Log("Levelbuilder failed to init");
            return false;
        }

        wallTypeToPrefab.Add(TileType.Floor, floorPrefab);
        wallTypeToPrefab.Add(TileType.Wall, wallPrefab);
        wallTypeToPrefab.Add(TileType.Cover, coverPrefab);
        wallTypeToPrefab.Add(TileType.PlayerSpawn, playerPrefab);
        wallTypeToPrefab.Add(TileType.BasicEnemySpawn, basicEnemyPrefab);
        wallTypeToPrefab.Add(TileType.BasicRobotSpawn, basicRobotPrefab);
        wallTypeToPrefab.Add(TileType.CoverArea, coverAreaPrefab);
        wallTypeToPrefab.Add(TileType.Door, doorPrefab);
        wallTypeToPrefab.Add(TileType.TurretSpawn, turretPrefab);
        wallTypeToPrefab.Add(TileType.MeleeEnemy, meleeEnemyPrefab);

        if (parent == null)
            parent = GameObject.Instantiate<GameObject>(new GameObject());

        Debug.Log("Levelbuilder initialized successfully");
        
        return true;
    }

    public static void BuildLevel(TileType[,] tiles, Agent agent)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                GameObject.Instantiate(wallTypeToPrefab[tiles[i, j]], new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                if (tiles[i, j] == TileType.PlayerSpawn || tiles[i, j] == TileType.BasicEnemySpawn || tiles[i, j] == TileType.Door 
                    || tiles[i, j] == TileType.BasicRobotSpawn || tiles[i, j] == TileType.TurretSpawn || tiles[i, j] == TileType.MeleeEnemy)
                    GameObject.Instantiate(floorPrefab, new Vector3(i * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                if (tiles[i, j] == TileType.Cover)
                {
                    if (i + 1 < tiles.GetLength(0) - 1 && 
                        tiles[i + 1, j] != TileType.Wall && 
                        tiles[i + 1, j] != TileType.Cover && 
                        tiles[i + 1, j] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i + 1) * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                    if (i != 0 && 
                        tiles[i - 1, j] != TileType.Wall && 
                        tiles[i - 1, j] != TileType.Cover 
                        && tiles[i - 1, j] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i - 1) * 0.64f, j * 0.64f), Quaternion.identity, parent.transform);

                    if (j + 1 < tiles.GetLength(1) - 1 && 
                        tiles[i, j + 1] != TileType.Wall && 
                        tiles[i, j + 1] != TileType.Cover && 
                        tiles[i, j + 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3(i * 0.64f, (j + 1) * 0.64f), Quaternion.identity, parent.transform);

                    if (j != 0 && 
                        tiles[i, j - 1] != TileType.Wall &&
                        tiles[i, j - 1] != TileType.Cover && 
                        tiles[i, j - 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3(i * 0.64f, (j - 1) * 0.64f), Quaternion.identity, parent.transform);

                    //*********************************************

                    if (i + 1 < tiles.GetLength(0) - 1 &&
                        j + 1 < tiles.GetLength(1) - 1 &&
                        tiles[i + 1, j + 1] != TileType.Wall &&
                        tiles[i + 1, j + 1] != TileType.Cover &&
                        tiles[i + 1, j + 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i + 1) * 0.64f, (j + 1) * 0.64f), Quaternion.identity, parent.transform);

                    if (i != 0 &&
                        j + 1 < tiles.GetLength(1) - 1 &&
                        tiles[i - 1, j + 1] != TileType.Wall &&
                        tiles[i - 1, j + 1] != TileType.Cover
                        && tiles[i - 1, j + 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i - 1) * 0.64f, (j + 1) * 0.64f), Quaternion.identity, parent.transform);

                    if (i != 0 &&
                        j != 0 &&
                        tiles[i - 1, j - 1] != TileType.Wall &&
                        tiles[i - 1, j - 1] != TileType.Cover &&
                        tiles[i - 1, j - 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i - 1) * 0.64f, (j - 1) * 0.64f), Quaternion.identity, parent.transform);

                    if (i + 1 < tiles.GetLength(0) - 1 &&
                        j != 0 &&
                        tiles[i + 1, j - 1] != TileType.Wall &&
                        tiles[i + 1, j - 1] != TileType.Cover &&
                        tiles[i + 1, j - 1] != TileType.CoverArea)
                        GameObject.Instantiate(wallTypeToPrefab[TileType.CoverArea], new Vector3((i + 1) * 0.64f, (j - 1) * 0.64f), Quaternion.identity, parent.transform);

                }
            }
        }
    }
}
