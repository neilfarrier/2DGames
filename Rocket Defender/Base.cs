using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public float health;
    public TextMeshProUGUI livesText;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 3f;
    }

    public void Damage(int amount)
    {
        health -= amount;
        livesText.text = health.ToString();
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        spriteRenderer.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
    }
}
