using System.Collections;
using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float duration = 2f;
    private Vector2 moveDir;

    private void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            moveDir = (player.position - transform.position).normalized;
        }

        Destroy(gameObject, duration + 3f);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.Translate(moveDir * velocidad * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player");
        }
    }
}

