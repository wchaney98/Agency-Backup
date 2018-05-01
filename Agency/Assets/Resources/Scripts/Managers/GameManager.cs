using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class GameManager : MonoBehaviour
{
    public GameObject DeathText;
    public GameObject SpecialCooldownPanel;
    public Text EnemiesRemainingText;
    float deadTimer = 0f;

    private PlayerController player;
    private Image specialFillImage;
    private Text specialText;

    private void Start()
    {
        CurrentMissionData.Reset();
        CurrentMissionData.MoneyEarned = PersistentData.Instance.CurrentContract.MoneyAward;
        CurrentMissionData.ReputationEarned = PersistentData.Instance.CurrentContract.ReputationAward;

        LevelBuilder.Inititialize();
        LevelBuilder.BuildLevel(PersistentData.Instance.CurrentContract.Tiles, PersistentData.Instance.CurrentAgent);

        EnemiesRemainingText.text = GameObject.FindObjectsOfType<AEnemy>().Length.ToString();
        EventManager.Instance.StartListening("EnemyDied", ProcessEnemyDied);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        specialFillImage = SpecialCooldownPanel.GetComponent<Image>();
        specialText = SpecialCooldownPanel.transform.Find("SpecialText").gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        UpdateSpecialCooldownVisual();

        if (player == null)
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

    private void UpdateSpecialCooldownVisual()
    {
        if (player == null)
        {
            return;
        }

        float fill = Mathf.Clamp01(player.specialCooldownTimer / player.specialCooldown);
        specialFillImage.fillAmount = fill;

        if (fill == 1)
        {
            specialText.color = Color.green;
        }
        else
        {
            specialText.color = Color.red;
        }
    }

    private void ProcessEnemyDied(EventParam e)
    {
        if (EnemiesRemainingText != null)
            EnemiesRemainingText.text = GameObject.FindObjectsOfType<AEnemy>().Length.ToString();
    }

    private void OnDestroy()
    {
        EventManager.Instance.StopListening("EnemyDied", ProcessEnemyDied);
        PlayerData.Instance.Save();
        Debug.Log("Save");
    }
}