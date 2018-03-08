using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    public GameObject DeathText;
    float deadTimer = 0f;

    private void Start()
    {
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
                SceneManager.LoadScene("MainGame");
            }
        }
        else
        {
            DeathText.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PlayerData.Instance.Save();
        Debug.Log("Save");
    }
}