using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float Deactivate = 3f;
    public float Activate = 3f;
    public float Stop = 3f;

    private bool PlayerOn = false;
    private float timer = 0f;
    private float playerTimer = 0f;
    private bool PlatformActive = true;
    private bool ReactivateM = false;
    private Collider2D platCollider;
    private Renderer platRenderer;

    private void Awake()
    {
        platCollider = GetComponent<Collider2D>();
        platRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (PlayerOn)
        {
            playerTimer += Time.deltaTime;
            Debug.Log("Tiempo sobre la plataforma: " + playerTimer.ToString("F2") + " segundos");

            if (playerTimer >= Stop)
            {
                if (PlatformActive)
                {
                    timer += Time.deltaTime;
                    Debug.Log("Tiempo hasta desactivación: " + (Deactivate - timer).ToString("F2") + " segundos");

                    if (timer >= Deactivate)
                    {
                        if (platCollider != null) platCollider.enabled = false;
                        if (platRenderer != null) platRenderer.enabled = false;

                        PlatformActive = false;
                        timer = 0f;
                        ReactivateM = true;
                        Debug.Log("Plataforma desactivada");
                    }
                }
            }
        }
        else if (ReactivateM)
        {
            timer += Time.deltaTime;
            Debug.Log("Tiempo hasta reactivación: " + (Activate - timer).ToString("F2") + " segundos");

            if (timer >= Activate)
            {
                if (platCollider != null) platCollider.enabled = true;
                if (platRenderer != null) platRenderer.enabled = true;

                PlatformActive = true;
                timer = 0f;
                ReactivateM = false;
                Debug.Log("Plataforma reactivada");
            }
        }
        else
        {
            playerTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerOn = true;
            Debug.Log("Jugador ha entrado en la plataforma");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerOn = false;
            playerTimer = 0f;
            Debug.Log("Jugador ha salido de la plataforma");
        }
    }
}

