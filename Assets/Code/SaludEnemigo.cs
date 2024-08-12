using UnityEngine;

public class SaludEnemigo : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakingDamage(float damage) //Se llama cuando el jugador tenga que recibir daño.
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
