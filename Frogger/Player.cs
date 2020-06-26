using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public delegate void PlayerHandler();
    public event PlayerHandler OnPlayerMoved;
    public event PlayerHandler OnPlayerEscaped;

    public float jumpDistance = 0.32f;

    private bool _jumped;
    private Vector3 _startingPosition;

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (!_jumped)
        {
            Vector2 targetPosition = Vector2.zero;
            bool tryingToMove = false;

            if(horizontalMovement != 0)
            {
                tryingToMove = true;
                targetPosition = new Vector2(transform.position.x + (horizontalMovement > 0 ? jumpDistance : -jumpDistance), transform.position.y);
            }
            if(verticalMovement != 0)
            {
                tryingToMove = true;
                targetPosition = new Vector2(transform.position.x, transform.position.y + (verticalMovement > 0 ? jumpDistance : -jumpDistance));
            }

            Collider2D hitCollider =  Physics2D.OverlapCircle(targetPosition, 0.1f);
            if(tryingToMove == true && (hitCollider == null || hitCollider.GetComponent<Enemy>() != null))
            {
                transform.position = targetPosition;
                _jumped = true;
                GetComponent<AudioSource>().Play();
                if (OnPlayerMoved != null)
                {
                    OnPlayerMoved();
                }
            }
        }
        else
        {
            if (horizontalMovement == 0.0f && verticalMovement == 0.0f)
                _jumped = false;
        }

        if(transform.position.y < -(Screen.height / 100f) / 2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + jumpDistance);
        }

        if (transform.position.y > (Screen.height / 100f) / 2f)
        {
            transform.position = _startingPosition;
            if(OnPlayerEscaped != null)
            {
                OnPlayerEscaped();
            }
        }

        if (transform.position.x < -(Screen.width / 100f) / 2f)
        {
            transform.position = new Vector3(transform.position.x + jumpDistance, transform.position.y);
        }

        if (transform.position.x > (Screen.width / 100f) / 2f)
        {
            transform.position = new Vector3(transform.position.x - jumpDistance, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Menu");
        }
    }
}
