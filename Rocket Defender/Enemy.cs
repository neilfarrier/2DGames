using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public AudioClip hitAudio;

    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed);
    }

    public void AssignTarget(Base target, float speed)
    {
        rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            collision.gameObject.GetComponent<Base>().Damage(1);
            audioSource.PlayOneShot(hitAudio);
            Debug.Log("Base hit");
        }

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
