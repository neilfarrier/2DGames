using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] sfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sfx[0]);
        Destroy(gameObject, 3f);
    }

    public void Launch(Vector2 direction, float speed)
    {
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        audioSource.PlayOneShot(sfx[1]);

        Destroy(this.gameObject, 1f);
    }
}
