using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerData : SingletonBehavior<PlayerData>
{
    public DataBlob DataBlob;
    
    public List<Contract> Contracts { get; private set; }
    public List<Agent> Agents { get; private set; }

    public float Reputation;
    public float Money;

    public int Day;

    private BinaryFormatter formatter = new BinaryFormatter();
    
    protected override void Init()
    {
        base.Init();
        DataBlob = new DataBlob();
        Contracts = new List<Contract>();
        Agents = new List<Agent>();
    }

    public void Save()
    {
        try
        {
            TransferDataToBlob();
            string fileName = "slot" + PersistentData.Instance.CurrentSaveSlot;
            using (StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/StreamingAssets/SaveData/" + fileName))
            {
                formatter.Serialize(streamWriter.BaseStream, DataBlob);
            }
        }
        catch (Exception e)
        {
            Debug.Log("PlayerData couldn't save with exception: " + e);
            throw;
        }
    }

    public void Load()
    {
        try
        {
            string fileName = "slot" + PersistentData.Instance.CurrentSaveSlot;
            using (StreamReader streamWriter = new StreamReader(Application.dataPath + "/StreamingAssets/SaveData/" + fileName))
            {
                DataBlob = (DataBlob)formatter.Deserialize(streamWriter.BaseStream);
            }
            TransferDataFromBlob();
        }
        catch (Exception e)
        {
            Debug.Log("PlayerData couldn't load with exception: " + e);
            throw;
        }
    }

    private void TransferDataToBlob()
    {
        DataBlob.Agents = Agents;
        DataBlob.Contracts = Contracts;
        DataBlob.Money = Money;
        DataBlob.Reputation = Reputation;
        DataBlob.Day = Day;
    }

    private void TransferDataFromBlob()
    {
        Agents = DataBlob.Agents;
        Contracts = DataBlob.Contracts;
        Money = DataBlob.Money;
        Reputation = DataBlob.Reputation;
        Day = DataBlob.Day;
    }
    
    // Emergency exit?
    private void OnDestroy()
    {
        // Save data
    }
}