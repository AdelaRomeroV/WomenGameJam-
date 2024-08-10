using UnityEngine;

public class StateController : MonoBehaviour
{
    public float changeInterval = 2f;
    public GameObject[] listaDeEstados;

    private int estadoActual = -1;
    private SpawnController spawnController;
    private float timer;

    private void Awake()
    {
        spawnController = GetComponent<SpawnController>();
    }

    private void Start()
    {
        timer = changeInterval;
        SetRandomState();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetRandomState();
            timer = changeInterval;
        }
    }

    private void SetRandomState()
    {
        int newEstado = Random.Range(0, listaDeEstados.Length);

        if (estadoActual != newEstado)
        {
            for (int i = 0; i < listaDeEstados.Length; i++)
            {
                listaDeEstados[i].SetActive(i == newEstado);
            }

            estadoActual = newEstado;

            if (estadoActual == 1)
            {
                spawnController.SpawnAttacks(); 
            }
        }
    }
}
