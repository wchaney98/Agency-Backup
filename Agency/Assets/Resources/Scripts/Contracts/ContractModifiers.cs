using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ContractModifiers
{
    public static int DefaultMapSize = 50;
    public static int DefaultEnemyDensity = 5; // per room

    public int NumFloors { get; set; }
    public float MapSize { get; set; }
    public bool Turrets { get; set; }
    public bool MeleeEnemies { get; set; }
    public bool RobotEnemies { get; set; }
    public int EnemyArmor { get; set; }
    public float FriendlyCoverSpawnChance { get; set; }
    public float EnemyDensity { get; set; }
}
