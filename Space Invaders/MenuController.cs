﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
