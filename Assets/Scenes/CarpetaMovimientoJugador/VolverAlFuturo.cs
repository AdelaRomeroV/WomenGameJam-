using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolverAlFuturo : MonoBehaviour
{
    public MovimientoJugador movimientoJugador;
    public bool isRewinding = false;
    public float recordTime; //Por si acaso ponle lo mismo que el de abajo.
    public float rewindDuration = 5f; // Duración del rewind en segundos
    Rigidbody2D rb;
    List<PuntoDeTiempo> pointsInTime;

    //NOTA : EN EL MOMENTO DE PUSHEO ACTUAL DEL TRABAJO SOLO SE LE ESTA APLICANDO AL JUGADOR LA POSIBILIDAD DE REGRESAR EN EL TIEMPO CUANDO SU VIDA LLEGA A 0 UN TOTAL DE 3 VECES, DESPUES ES DESTRUIDO.
    //NOTA 1: ESTE CODIGO SE ESTA USANDO PARA TRATAR DE LOGRAR LO QUE SE BUSCA PARA LA VERSION DE TOULOUSE COMPRENDAN POFIS :C
    //NOTA 2: EN CASO DE UTILIZAR ESTE CODIGO, ESTE SE ACTIVA AL PRESIONAR LA TECLA *P* LO QUE GENERA LA REGRESION TEMPORAL DE TODOS LOS OBJETOS QUE POSEAN EL CODIGO (Incluido el Jugador en caso se le ponga este codigo, debido a que todos funcionan con el mismo codigo no esta implementado la funcion de retorno cuando la vida llega a 0 eso se esta viendo ahorita mismo).

    void Start()
    {
        pointsInTime = new List<PuntoDeTiempo>();
        rb = GetComponent<Rigidbody2D>();
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PuntoDeTiempo pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PuntoDeTiempo(transform.position, transform.rotation));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
        StartCoroutine(StopRewindAfterDelay());
    }

    IEnumerator StopRewindAfterDelay()
    {
        yield return new WaitForSeconds(rewindDuration);
        StopRewind();
    }

    public void StopRewind()
    {
        movimientoJugador.canMove = true;   
        isRewinding = false;
        rb.isKinematic = false;
    }
}
