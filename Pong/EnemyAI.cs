using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public Vector2 startingPosition = new Vector2(13.0f, 0.0f);

    private GameObject ball;
    private Vector2 ballPos;

    void Start()
    {
        transform.localPosition = (Vector3)startingPosition;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!ball)
            ball = GameObject.FindGameObjectWithTag("Ball");

        if(ballPos.y < transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
        }

        if(ballPos.y > transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
