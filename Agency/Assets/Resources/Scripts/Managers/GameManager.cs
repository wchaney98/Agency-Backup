using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class GameManager : MonoBehaviour
{
    public GameObject DeathText;
    float deadTimer = 0f;

    private void Start()
    {
        CurrentMissionData.Reset();
        CurrentMissionData.MoneyEarned = PersistentData.Instance.CurrentContract.MoneyAward;
        CurrentMissionData.ReputationEarned = PersistentData.Instance.CurrentContract.ReputationAward;

        LevelBuilder.Inititialize();
        LevelBuilder.BuildLevel(PersistentData.Instance.CurrentContract.Tiles, PersistentData.Instance.CurrentAgent);
    }

    private void Update()
    {
        if (GameObject.FindObjectOfType<PlayerController>() == null)
        {
            DeathText.SetActive(true);
            deadTimer += Time.deltaTime;
            if (deadTimer >= 1.5f)
            {
                SceneManager.LoadScene("ManagementScene");
            }
        }
        else
        {
            DeathText.SetActive(false);
        }

        if (GameObject.FindObjectOfType<AEnemy>() == null)
        {
            DeathText.GetComponent<Text>().text = "Mission Complete";
            DeathText.SetActive(true);
            deadTimer += Time.deltaTime;
            if (deadTimer >= 1.5f)
            {
                SceneManager.LoadScene("LevelEndScene");
            }
        }
        else
        {
            CurrentMissionData.TimeTaken += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        PlayerData.Instance.Save();
        Debug.Log("Save");
    }
}