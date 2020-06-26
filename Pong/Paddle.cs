using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 1f;
    public int playerIndex = 1;

    private Ball ball;

    private void Start()
    {
        
    }

    void Update()
    {
        ball = GameObject.FindObjectOfType<Ball>();

        if (gameObject.tag == "Player")
            MovePlayerPaddle();
        else
            MoveComputerPaddle();
    }

    private void MovePlayerPaddle()
    {
        float verticalMovement = Input.GetAxis("Vertical" + playerIndex);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalMovement * speed);
    }

    private void MoveComputerPaddle()
    {
        float paddlePosy = transform.position.y;

        if (paddlePosy < ball.ballPosY)
            transform.position += Vector3.up * speed * Time.deltaTime;
        else
            transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
