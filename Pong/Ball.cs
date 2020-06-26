using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minXSpeed = 0.8f;
    public float maxXSpeed = 1.2f;
    public float minYSpeed = 0.8f;
    public float maxYSpeed = 1.2f;
    public float difficultyMultiplier = 1.3f;
    public float ballPosY;

    private Rigidbody2D ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        ballRigidbody.velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed) * (Random.value > 0.5f ? -1 : 1), Random.Range(minYSpeed, maxYSpeed) * (Random.value > 0.5f ? -1 : 1));
    }

    void Update()
    {
        ballPosY = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Limit")
        {
            GetComponent<AudioSource>().Play();
            if (other.transform.position.y > transform.position.y && ballRigidbody.velocity.y > 0)
            {
                ballRigidbody.velocity = new Vector2(ballRigidbody.velocity.x, -ballRigidbody.velocity.y);
            }
            if (other.transform.position.y < transform.position.y && ballRigidbody.velocity.y < 0)
            {
                ballRigidbody.velocity = new Vector2(ballRigidbody.velocity.x, -ballRigidbody.velocity.y);
            }
        }
        else if (other.tag == "Player" || other.tag == "Paddle")
        {
            GetComponent<AudioSource>().Play();
            if (other.transform.position.x < transform.position.x && ballRigidbody.velocity.x < 0)
            {
                ballRigidbody.velocity = new Vector2(-ballRigidbody.velocity.x * difficultyMultiplier, ballRigidbody.velocity.y * difficultyMultiplier);
            }
            if (other.transform.position.x > transform.position.x && ballRigidbody.velocity.x > 0)
            {
                ballRigidbody.velocity = new Vector2(-ballRigidbody.velocity.x * difficultyMultiplier, ballRigidbody.velocity.y * difficultyMultiplier);
            }
        }
    }
}
