﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonBehavior : MonoBehaviour
{
    public string BackScene = "MainMenu";
    
    public void Back()
    {
        SceneManager.LoadScene(BackScene);
    }
}
