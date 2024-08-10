using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyW : MonoBehaviour
{
    public GameObject Punto1;
    public GameObject Punto2;
    private Transform Cpunto;
    private Rigidbody2D rb2D;
    [SerializeField] float speed;
    [SerializeField] float RadioDeteccion;
    private bool detectarPlayer = false;
    private Transform player;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Cpunto = Punto2.transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (detectarPlayer)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb2D.velocity = directionToPlayer * speed;
        }
        else
        {
            Vector2 direction = (Cpunto.position - transform.position).normalized;
            rb2D.velocity = direction * speed;

            if (Vector2.Distance(transform.position, Cpunto.position) < 0.1f)
            {
                FlipDirection();
                Cpunto = (Cpunto == Punto1.transform) ? Punto2.transform : Punto1.transform;
            }
        }
    }

    private void FlipDirection()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (Punto1 != null && Punto2 != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Punto1.transform.position, 0.5f);
            Gizmos.DrawWireSphere(Punto2.transform.position, 0.5f);
            Gizmos.color = Color.black;
            Gizmos.DrawLine(Punto1.transform.position, Punto2.transform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detectarPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detectarPlayer = false;
        }
    }
}

