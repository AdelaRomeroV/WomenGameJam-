using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator; // Referencia al Animator

    [Header("Movement/Movimiento")]
    [SerializeField, Range(0, 10)] private float acceleration;
    [SerializeField, Range(0, 15)] private float speed;
    private float horizontal;
    public bool canMove = true;

    [Header("Jump/Salto")]
    [SerializeField] private Transform pos; //Transform usado como detector de posición
    [SerializeField] private float checkRadius; // Radio de detección
    [SerializeField] private LayerMask whatIsGround; // Qué es suelo
    [SerializeField] private float jpower; // Fuerza de salto
    [SerializeField] private float jumpTime; // Duración máxima del salto
    private float jumpTimeCounter; // Contador de tiempo de salto
    private bool isGrounded; // Si está en el suelo
    private bool isJumping; // Si está saltando

    [Header("Disparo")]
    [SerializeField] private GameObject bullet; // Prefab de la bala
    [SerializeField] private float destroyTime; // Tiempo de destrucción del clon
    [SerializeField] private float bulletPower; // Potencia de la bala
    [SerializeField] private float shootRate; // Velocidad de disparo
    private float nextShootTime = 0f; // Tiempo de espera entre disparos

    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            isGrounded = Physics2D.OverlapCircle(pos.position, checkRadius, whatIsGround);
            animator.SetBool("OnGround", isGrounded);
            animator.SetInteger("Move", (int)horizontal);
            HandleJumping();
            HandleShooting();
        }
    }

    private void HandleJumping()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            StartJump();
        }

        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            ContinueJump();
        }

        if (Input.GetKeyUp(KeyCode.W) || rb.velocity.y < 0)
        {
            EndJump();
        }
    }

    private void StartJump()
    {
        animator.SetTrigger("JumpStart");
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jpower;
    }

    private void ContinueJump()
    {
        if (jumpTimeCounter > 0)
        {
            rb.velocity = Vector2.up * jpower;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            EndJump();
        }
    }

    private void EndJump()
    {
        if (isJumping)
        {
            animator.SetTrigger("JumpEnd");
            isJumping = false;
        }
    }

    private void HandleShooting()
    {
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

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
    }
}
