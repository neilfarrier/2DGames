using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject ballPrefab;
    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    public float scoreCoordinates = 3.4f;

    private Ball currentBall;
    private int score1 = 0;
    private int score2 = 0;

    [Header("End Game Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;


    void Start()
    {
        SpawnBall();
        Time.timeScale = 1;
        endGameScreen.SetActive(false);
    }

    void SpawnBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, transform);

        currentBall = ballInstance.GetComponent<Ball>();
        currentBall.transform.position = Vector3.zero;

        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }

    void Update()
    {
        if(score1 >= 9)
        {
        SetEndGameScreen(true, score1, score2);
        }
        if(score2 >= 9)
        {
        SetEndGameScreen(false, score1, score2);
        }

        if (currentBall != null)
        {
            if (currentBall.transform.position.x > scoreCoordinates)
            {
                score1++;
                Destroy(currentBall.gameObject);
                SpawnBall();
            }
            if (currentBall.transform.position.x < -scoreCoordinates)
            {
                score2++;
                Destroy(currentBall.gameObject);
                SpawnBall();
            }
        }
    }

    private void SetEndGameScreen(bool won, int score1, int score2)
    {
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "Player 1 Wins" : "Player 2 Wins";
        endGameHeaderText.color = won == true ? Color.red : Color.blue;
        endGameScoreText.text = score1 + " : " + score2;
        Time.timeScale = 0;
    }
}
