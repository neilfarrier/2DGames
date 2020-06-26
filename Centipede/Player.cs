using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.5f;
    public float horizontalLimit = 2.8f;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float shootingCooldown;

    private float _shootingTime;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

        if(transform.position.x > horizontalLimit)
        {
            transform.position = new Vector2(horizontalLimit, transform.position.y);
        }
        else if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector2(-horizontalLimit, transform.position.y);
        }

        _shootingTime -= Time.deltaTime;
        if(_shootingTime <= 0)
        {
        if(Input.GetAxis("Fire1") == 1f || Input.GetKeyDown(KeyCode.Space))
            {
                _shootingTime = shootingCooldown;

                GameObject bulletInstance = Instantiate(bulletPrefab);
                bulletInstance.transform.SetParent(transform.parent);
                bulletInstance.transform.position = transform.position;
                bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
                Destroy(bulletInstance, 5f);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Destroy(gameObject);
    }
}
