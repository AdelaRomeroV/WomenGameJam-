using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludJugador : MonoBehaviour
{

    [SerializeField] private float life; //Salud del jugador, en este caso 30.
    private float maxLife;
    private Renderer objectRenderer; 
    [SerializeField] private Color fullLifeColor = Color.white; //Color que tiene el objeto cuando su vida esta al maximo, en este caso blanco (white)
    [SerializeField] private Color lowLifeColor = Color.black; // Color cuando la vida es baja, en este caso negro (black)
    public Collider2D collider2D;
    public VolverAlFuturo volverAlFuturo;
    public MovimientoJugador movimientoJugador;
    public Animator animator;
    public float chances; //Oportunidades que se le dan al jugador.

    private void Start()
    {
        maxLife = life;
        objectRenderer = GetComponent<Renderer>();
        collider2D = GetComponent<Collider2D>(); // Obtener el componente Collider2D del jugador
        UpdateColor();
        volverAlFuturo = GetComponent<VolverAlFuturo>();
        movimientoJugador = GetComponent<MovimientoJugador>();
        animator = GetComponent<Animator>();
    }

    public void TakingDamage(float damage) //Se llama cuando el jugador tenga que recibir daño.
    {
        life -= damage;
        UpdateColor(); //Cambia el color cuando este es lastimado.
        if (life <= 0)
        {
            Death();
        }
    }

    public void ReceiveHealing(float heal)
    {
        life += heal;
        if (life > maxLife)
        {
            life = maxLife;
        }
        UpdateColor(); //Cambia el color cuando se cura.
    }

    private void UpdateColor()
    {
        float lifePercentage = life / maxLife; //Se calcula el porcentaje de la vida
        objectRenderer.material.color = Color.Lerp(lowLifeColor, fullLifeColor, lifePercentage); 
    }

    private void Death() // La consecuencia de que life llegue a 0 (por motivos de prueba actualmente es Destroy, se reemplazará).
    {
        if (chances > 0)
        {
            animator.SetTrigger("IsDeath");
            movimientoJugador.canMove = false;
            collider2D.enabled = false; // Desactiva el collider para hacer al jugador intangible
            chances--;
            volverAlFuturo.StartRewind();
            life = maxLife;
            UpdateColor();
        }
        else
        {
            animator.SetTrigger("IsDeath");
            Destroy(gameObject);
        }

    }
}
