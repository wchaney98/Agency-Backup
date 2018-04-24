using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScene : MonoBehaviour
{
    private void Start()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<size=56>Game Over</size>");
        sb.AppendLine("<size=30>Final Reputation: " + CurrentMissionData.ReputationEarned + "</size>");
        sb.AppendLine("<size=30>Final Money: " + CurrentMissionData.MoneyEarned + "</size>");
        GetComponent<Text>().text = sb.ToString();
        PlayerData.Instance.DeleteCurrentSlotData();
        Debug.Log("Delete");
    }
}