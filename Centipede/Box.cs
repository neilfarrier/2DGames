using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float shrinkAmount = 0.5f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            transform.localScale = new Vector2(transform.localScale.x - shrinkAmount, transform.localScale.y - shrinkAmount);
            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
