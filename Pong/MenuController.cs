using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void On1PlayerButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game 1");
    }

    public void On2PlayerButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
