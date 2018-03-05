using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotButtonBehavior : MonoBehaviour
{
    public int SlotNumber;
    
    private Text text;
    private string slotName;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        slotName = text.text;

        // Check data file
        if (!File.Exists(Application.dataPath + "/StreamingAssets/SaveData/" + text.text))
        {
            text.text = "empty";
        }
    }

    public void OnClick()
    {
        PersistentData.Instance.CurrentSaveSlot = SlotNumber;
        if (File.Exists(Application.dataPath + "/StreamingAssets/SaveData/" + slotName))
        {
            PlayerData.Instance.Load();
        }
        else
        {
            PlayerData.Instance.Save();
        }
        SceneManager.LoadScene("ManagementScene");
    }
}