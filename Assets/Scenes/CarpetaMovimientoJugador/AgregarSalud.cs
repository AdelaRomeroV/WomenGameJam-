using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgregarSalud : MonoBehaviour
{
    [SerializeField] private float healthGiven; //Salud otorgada al jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<SaludJugador>().ReceiveHealing(healthGiven);
            Destroy(gameObject);
        }
    }

  
   
}
