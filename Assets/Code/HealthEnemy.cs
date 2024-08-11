using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public int lifes = 5;
    public int maxLifes = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lifes--;
            if (lifes <= 0) Destroy(gameObject);
        }
    }
}

