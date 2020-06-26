using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public float shootingInterval = 3f;
    public float shootingSpeed = 2f;
    public GameObject enemyMissilePrefab;
    public GameObject enemyContainer;
    public Player player;
    public float maxMovingInterval = 0.4f;
    public float minMovingInterval = 0.05f;
    public float movingDistance = 0.1f;
    public float horizontalLimit = 2.5f;
    public int currentScore;

    private float shootingTimer;
    private float movingTimer;
    private float movingDirection = 1f;
    private float movingInterval;
    private int enemyCount;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        movingInterval = maxMovingInterval;
        shootingTimer = shootingInterval;
        enemyCount = GetComponentsInChildren<Enemy>().Length;
    }

    void Update()
    {
        int currentEnemyCount = GetComponentsInChildren<Enemy>().Length;

        shootingTimer -= Time.deltaTime;
        if (currentEnemyCount > 0 && shootingTimer <= 0f)
        {
            shootingTimer = shootingInterval;

            Enemy[] enemies = GetComponentsInChildren<Enemy>();
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

            GameObject missileInstance = Instantiate(enemyMissilePrefab);
            missileInstance.transform.SetParent(transform);
            missileInstance.transform.position = randomEnemy.transform.position;
            missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootingSpeed);
            Destroy(missileInstance, 5f);
        }

        movingTimer -= Time.deltaTime;
        if (movingTimer <= 0f)
        {
            float difficulty = 1f - (float)currentEnemyCount / enemyCount;

            movingInterval = maxMovingInterval - (maxMovingInterval - minMovingInterval) * difficulty;
            movingTimer = movingInterval;

            enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x + (movingDistance * movingDirection), enemyContainer.transform.position.y);

            if (movingDirection > 0)
            {
                float rightMostPosition = 0f;
                foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
                {
                    if (enemy.transform.position.x > rightMostPosition)
                    {
                        rightMostPosition = enemy.transform.position.x;
                    }
                }

                if (rightMostPosition > horizontalLimit)
                {
                    movingDirection *= -1;
                    enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x, enemyContainer.transform.position.y - movingDistance);
                }
            }
            else
            {
                float leftMostPosition = 0f;
                foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
                {
                    if (enemy.transform.position.x < leftMostPosition)
                    {
                        leftMostPosition = enemy.transform.position.x;
                    }
                }

                if (leftMostPosition < -horizontalLimit)
                {
                    movingDirection *= -1;
                    enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x, enemyContainer.transform.position.y - movingDistance);
                }
            }
        }

        if (currentEnemyCount == 0)
        {
            SceneManager.LoadScene("Game");
            Time.timeScale = 1;
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        GameUi.instance.UpdateScoreText(currentScore);
    }
}
