using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float horizontalLimit = 2.5f;
    public GameObject missilePrefab;
    public float firingSpeed = 3f;
    public float firingCooldownDuration = 1f;
    public GameObject explosionPrefab;
    public GameObject gameOverPanel;
    public AudioClip explosionAudio;

    private float cooldownTimer;
    private AudioSource audioSource;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

        if (transform.position.x > horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0 && Input.GetAxis("Fire1") == 1f)
        {
            cooldownTimer = firingCooldownDuration;

            GameObject missileInstance = Instantiate(missilePrefab);
            missileInstance.transform.SetParent(transform.parent);
            missileInstance.transform.position = transform.position;
            missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, firingSpeed);
            Destroy(missileInstance, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyMissile" || other.tag == "Enemy")
        {
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.SetParent(transform.parent.parent);
            explosionInstance.transform.position = transform.position;

            Destroy(explosionInstance, 1.5f);
            Destroy(gameObject);
            Destroy(other.gameObject);
            Time.timeScale = 0;
        }
    }
}
