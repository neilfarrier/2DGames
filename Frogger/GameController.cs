using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameOverText;
    public float difficultyIncrease = 1.2f;

    private float _highestPosition;
    private int _score = 0;
    private int _level = 1;
    private float _restartTimer = 3f;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        player.OnPlayerMoved += OnPlayerMoved;
        player.OnPlayerEscaped += OnPlayerEscaped;

        _highestPosition = player.transform.position.y;
    }

    void Update()
    {
        if(player == null)
        {
            gameOverText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            levelText.gameObject.SetActive(false);

            _restartTimer -= Time.deltaTime;
            if (_restartTimer <= 0f)
                SceneManager.LoadScene("Main");
        }
    }

    void OnPlayerMoved()
    {
        if(player.transform.position.y > _highestPosition)
        {
            _highestPosition = player.transform.position.y;
            _score++;
            scoreText.text = "Score: " + _score;
        }
    }

    void OnPlayerEscaped()
    {
        _highestPosition = player.transform.position.y;
        _level++;
        levelText.text = "Level: " + _level;

        foreach(Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            enemy.speed *= difficultyIncrease;
        }
    }
}
