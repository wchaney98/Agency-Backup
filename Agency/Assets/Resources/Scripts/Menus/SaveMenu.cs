using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    private void Start()
    {
        PlayerData.Instance.UnloadCurrentData();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
