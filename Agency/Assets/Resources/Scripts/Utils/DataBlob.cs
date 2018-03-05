using System;
using System.Collections.Generic;

/// <summary>
/// Houses the save data since Monobevahiors can't be serialized
/// </summary>
[Serializable]
public class DataBlob
{
    public List<Contract> Contracts { get; set; }
    public List<Agent> Agents { get; set; }

    public float Reputation;
    public float Money;

    public int Day;
}