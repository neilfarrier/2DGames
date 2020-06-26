using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int scoreToGive = 5;

    public AudioSource explosionSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerMissile")
        {
            GetComponent<AudioSource>().Play();
            GameController.instance.AddScore(scoreToGive);
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.SetParent(transform.parent);
            explosionInstance.transform.position = transform.position;

            Destroy(explosionInstance, 1.5f);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
