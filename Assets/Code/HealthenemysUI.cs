using UnityEngine;
using UnityEngine.UI;

public class Healthenemys : MonoBehaviour
{
    [SerializeField] private Image barra;
    [SerializeField] private HealthEnemy entidad;
    [SerializeField] private Collider2D triggerArea;

    private void Start()
    {
        barra.enabled = false;
    }

    private void Update()
    {
        float vida = entidad.lifes / (float)entidad.maxLifes;
        barra.fillAmount = vida;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barra.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            barra.enabled = false;
        }
    }
}
