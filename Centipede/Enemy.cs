using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public delegate void KillHandler(Enemy enemy);
    public event KillHandler OnKill;

    public float speed = 2f;
    public float horizontalLimit = 2.8f;
    public int tileSize = 32;
    public Enemy nextEnemy;

    private float _movingDirection = 1f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * _movingDirection, 0);
    }

    void Update()
    {
        if(_movingDirection > 0 && transform.position.x > horizontalLimit)
        {
            MoveDown();
        }
        else if (_movingDirection < 0 && transform.position.x < -horizontalLimit)
        {
            MoveDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Box")
        {
            MoveDown();
        } else if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            if(OnKill != null)
            {
                OnKill(this);
            }
        }
    }

    void MoveDown()
    {
        if (transform.position.y < -(Screen.height / 100f) / 2f)
        {
            SceneManager.LoadScene("Main");
        }

        _movingDirection *= -1;
        transform.position = new Vector2(transform.position.x, transform.position.y - tileSize / 100f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * _movingDirection, 0);
    }

    public void ChangeDirection()
    {
        _movingDirection *= -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * _movingDirection, 0);
    }
}
