using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float levelTime;
    private int totalPoints;
    private bool isRoundOver;
    private float difficultyModifier = 1.5f;

    [SerializeField]
    private Transform gameMenu;

    [SerializeField]
    private TextMeshProUGUI gameMenuText;

    [SerializeField]
    private Bases bases;

    [SerializeField]
    private EnemySpawner spawner;

    [SerializeField]
    private TextMeshProUGUI timerText;

    void Update()
    {
        levelTime -= Time.deltaTime;
        if(levelTime <= 0 && !isRoundOver)
        {
            CompleteRound();
        }
        timerText.text = levelTime.ToString("00");
    }

    void CompleteRound()
    {
        isRoundOver = true;
        totalPoints += bases.BaseCount;
        spawner.Stop();
        gameMenu.gameObject.SetActive(true);
        gameMenuText.text = string.Format("{0} bases remaining\n{1} total points", bases.BaseCount, totalPoints);
        Time.timeScale = 0;
    }

    public void StartNextRound()
    {
        isRoundOver = false;
        Time.timeScale = 1 * difficultyModifier;
        levelTime = 30f * difficultyModifier;
        StartCoroutine(spawner.StartSpawning());
        gameMenu.gameObject.SetActive(false);
    }
}
