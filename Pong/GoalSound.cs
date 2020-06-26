using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSound : MonoBehaviour
{
    public AudioSource goalSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
            GetComponent<AudioSource>().Play();
    }
}
