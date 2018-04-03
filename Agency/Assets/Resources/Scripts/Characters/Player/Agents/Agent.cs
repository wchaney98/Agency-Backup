using System;
using System.Text;
using UnityEngine;

[Serializable]
public enum AgentType
{
    Standard, // Default pistol w/ nades
    Breacher, // Flashbangs + SMG
    Elite,    // AR + combat roll
    Joker,    // Grenades and shotgun
    Riot      // Riot (reflective?) shield + pistol
}

[Serializable]
public class Agent
{
    public string Title;
    public StringBuilder Description;
    public AgentType AgentType;

    // TODO impl these vars 
    public int XP;
    public int Level;
    public int Speed;
    public int Power;
    
    public float MoveSpeed = 3.3f;
    
    public string BulletPrefabPath;
    public string SpecialPrefabPath; // special.Update

    public float PrimaryCooldown = 0.5f;
    public float SpecialCooldown = 1f;
}