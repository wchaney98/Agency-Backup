using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndScene : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = CurrentMissionData.GetString();
        PlayerData.Instance.Money += CurrentMissionData.MoneyEarned;
        PlayerData.Instance.Reputation += CurrentMissionData.ReputationEarned;
        PlayerData.Instance.Save();
        Debug.Log("Save");
    }
}
