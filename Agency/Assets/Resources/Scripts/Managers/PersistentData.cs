using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data that must persist between scene switches but not necessarily between game sessions
/// </summary>
public class PersistentData : SingletonBehavior<PersistentData>
{
    public Contract CurrentContract { get; set; }
    public Agent CurrentAgent { get; set; }
    public int CurrentSaveSlot;

    private void Start()
    {
        LevelBuilder.Inititialize();
    }
}