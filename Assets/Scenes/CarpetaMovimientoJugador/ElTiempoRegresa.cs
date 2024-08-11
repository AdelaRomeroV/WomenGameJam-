using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElTiempoRegresa : MonoBehaviour
{
    public bool isRewinding = false;
    public float recordTime;
    public float rewindDuration = 5f; // Duración del rewind en segundos
    Rigidbody2D rb;
    List<PuntoDeTiempo> pointsInTime;


    //NOTA : ESTE CODIGO SE ESTA USANDO PARA PROBAR POSIBILIDADES PARA DESPUES, USAR VolverAlFuturo en el Jugador y solamente el Jugador.


    void Start()
    {
        pointsInTime = new List<PuntoDeTiempo>();
        rb = GetComponent<Rigidbody2D>();
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
        isRewinding = false;
        rb.isKinematic = false;
    }
}
