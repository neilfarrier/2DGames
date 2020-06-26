using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUi : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject player;

    public static GameUi instance;
    void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    public void Update()
    {
        if(player == null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
