using UnityEngine;
using UnityEngine.UI;

public class Healthenemys : MonoBehaviour
{
    [SerializeField] private Image barra;
    [SerializeField] private HealthEnemy entidad;

    private void Update()
    {
        float vida = entidad.lifes / (float)entidad.maxLifes;
        barra.fillAmount = vida;
    }
}
