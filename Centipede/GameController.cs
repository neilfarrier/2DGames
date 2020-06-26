using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject boxPrefab;
    public int tileSize = 32;
    public float boxChance = 0.1f;
    public Player player;
    public GameObject enemyPrefab;
    public int enemySize;
    public GameObject pauseScreen;
    public TextMeshProUGUI remainingEnemiesText;
    public int remainingEnemies;

    private List<Enemy> enemies;
    private bool _killedEnemy;

    private void Awake()
    {
        enemySize = Random.Range(4, 12);
        pauseScreen.gameObject.SetActive(false);
    }
    void Start()
    {
        remainingEnemies = enemySize;
        enemies = new List<Enemy>();
        // Procedural box generation
       for(int y = Screen.height / 2 - tileSize; y > -Screen.height / 2 + tileSize * 2; y -= tileSize)
        {
            for(int x = Screen.width / 2 - tileSize; x > -Screen.width / 2 + tileSize; x -= tileSize)
            {
                if(Random.value < boxChance)
                {
                    GameObject boxInstance = Instantiate(boxPrefab);
                    boxInstance.transform.SetParent(transform);
                    boxInstance.transform.position = new Vector2((x - tileSize / 2) / 100f, (y - tileSize / 2) / 100f);
                }
            }
        }

       Enemy previousEnemy = null;
       for(int i = 0; i < enemySize; i++)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab);
            enemyInstance.transform.SetParent(transform);
            enemyInstance.transform.position = new Vector2(-i * tileSize / 100f, (Screen.height / 2 - tileSize / 2) / 100f);

            Enemy enemy = enemyInstance.GetComponent<Enemy>();
            enemy.OnKill += OnEnemyKill;

            if(previousEnemy != null)
            {
                previousEnemy.nextEnemy = enemy;
            }
            else
            {
                enemies.Add(enemy);
            }

            previousEnemy = enemy;
        }
    }

    void Update()
    {
        remainingEnemiesText.text = "Enemies Left: " + remainingEnemies;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        if(player == null)
        {
            SceneManager.LoadScene("Main");
        }

        if(enemies.Count == 0)
        {
            SceneManager.LoadScene("Main");
        }

        _killedEnemy = false;
    }

    void OnEnemyKill(Enemy enemy)
    {
        if(_killedEnemy == true)
        {
            return;
        }

        _killedEnemy = true;

        Enemy currentEnemy = enemy;
        if(enemy.nextEnemy != null)
        {
            List<Enemy> enemyString = new List<Enemy>();
            while(currentEnemy.nextEnemy != null)
            {
                enemyString.Add(currentEnemy);
                currentEnemy.ChangeDirection();
                currentEnemy = currentEnemy.nextEnemy;
            }
            enemyString.Add(currentEnemy);

            for(int i = enemyString.Count - 1; i > 0; i--)
            {
                enemyString[i].nextEnemy = enemyString[i - 1];
            }
            enemyString[0].nextEnemy = null;

            currentEnemy.ChangeDirection();
            enemies.Add(currentEnemy);
        }

        if(enemies.IndexOf(enemy) != -1)
        {
            enemies.Remove(enemy);
        }

        Destroy(enemy.gameObject);
        remainingEnemies--;
        remainingEnemiesText.text = "Enemies Left: " + remainingEnemies;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
