using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerData : SingletonBehavior<PlayerData>
{
    private DataBlob dataBlob;
    
    public List<Contract> Contracts { get; private set; }
    public List<Agent> Agents { get; private set; }

    public float Reputation;
    public float Money;

    public int Day;

    private BinaryFormatter formatter = new BinaryFormatter();
    
    protected override void Init()
    {
        base.Init();
        dataBlob = new DataBlob();
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
                formatter.Serialize(streamWriter.BaseStream, dataBlob);
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
                dataBlob = (DataBlob)formatter.Deserialize(streamWriter.BaseStream);
            }
            TransferDataFromBlob();
        }
        catch (Exception e)
        {
            Debug.Log("PlayerData couldn't load with exception: " + e);
            throw;
        }
    }

    public void DeleteCurrentSlotData()
    {
        try
        {
            string fileName = "slot" + PersistentData.Instance.CurrentSaveSlot;
            File.Delete(Application.dataPath + "/StreamingAssets/SaveData/" + fileName);
        }
        catch (Exception e)
        {
            Debug.Log("PlayerData couldn't delete with exception: " + e);
            throw;
        }
        UnloadCurrentData();
    }

    public void UnloadCurrentData()
    {
        dataBlob = new DataBlob();
        Agents.Clear();
        Contracts.Clear();
        Money = 0;
        Reputation = 0;
        Day = 0;
    }

    private void TransferDataToBlob()
    {
        dataBlob.Agents = Agents;
        dataBlob.Contracts = Contracts;
        dataBlob.Money = Money;
        dataBlob.Reputation = Reputation;
        dataBlob.Day = Day;
    }

    private void TransferDataFromBlob()
    {
        Agents = dataBlob.Agents;
        Contracts = dataBlob.Contracts;
        Money = dataBlob.Money;
        Reputation = dataBlob.Reputation;
        Day = dataBlob.Day;
    }
    
    // Emergency exit?
    private void OnDestroy()
    {
        // Save data
    }
}