using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    //[SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator; // Referencia al Animator

    [Header("Movement/Movimiento")]
    [SerializeField, Range(0, 10)] private float acceleration; //La aceleracion
    [SerializeField, Range(0, 15)] private float speed; //La velocidad
    private float horizontal;
    public bool canMove = true;

    [Header("Jump/Salto")]
    [SerializeField] private bool isGrounded; //Bool que marca si el jugador toca el suelo
    [SerializeField] Transform pos; //Transform que es utilizado como detector de posicion
    [SerializeField] private float checkRadius; //radio c:
    [SerializeField] private LayerMask whatIsGround; //Que es suelo?
    [SerializeField] private float jpower; //fuerza de salto
    [SerializeField] private float jumpTimeCounter; //un contador que resulta = al jumpTime
    [SerializeField] private float jumpTime; //un valor que podes variar a gusto :D
    [SerializeField] private bool isJumping; //bool que detecta si el jugador esta en pleno salto

    [Header("Disparo")]
    [SerializeField] private GameObject bullet; //Prefab que va a disparar
    [SerializeField] private float destroyTime; //Tiempo con el cual el clone sera destruido
    [SerializeField] private float bulletPower; //Potencia de la bala con la cual es lanzada
    [SerializeField] private float shootRate; //Velocidad para seguir disparando.
    private float nextShootTime = 0f; //Si se puede seguir disparando.

    void Update()
    {
        if (canMove)
        {
            //Movimiento y Salto

            horizontal = Input.GetAxisRaw("Horizontal");
            isGrounded = Physics2D.OverlapCircle(pos.position, checkRadius, whatIsGround);
            animator.SetInteger("Move", (int)horizontal);

            if (isGrounded && Input.GetKeyDown(KeyCode.W))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jpower;
            }

            if (Input.GetKey(KeyCode.W) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jpower;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
            }

            // Disparo
            if (Input.GetKey(KeyCode.J) && Time.time >= nextShootTime)
            {
                float direction = transform.localScale.x;
                Vector2 pos = transform.position + new Vector3(direction, 0, 0);
                GameObject clone = Instantiate(bullet, pos, Quaternion.identity);
                clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0) * bulletPower, ForceMode2D.Impulse);
                Destroy(clone, destroyTime);
                nextShootTime = Time.time + shootRate;
            }
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
    }
}
