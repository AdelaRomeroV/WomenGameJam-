using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int nSpawns = 3;
    [SerializeField] private float sRange = 10f;
    [SerializeField] private float destroyclones = 3f;
    private bool isSpawn = false;

    public void SpawnAttacks()
    {
        if (!isSpawn)
        {
            StartCoroutine(SpawnR());
        }
    }

    private IEnumerator SpawnR()
    {
        isSpawn = true; 

        for (int i = 0; i < nSpawns; i++)
        {
            Vector2 spawnPos = new Vector2
                (transform.position.x + Random.Range(-sRange, sRange),
                transform.position.y + Random.Range(-sRange, sRange));

            GameObject spawnedObj = Instantiate(prefab, spawnPos, Quaternion.identity);
            Destroy(spawnedObj, destroyclones);

            yield return new WaitForSeconds(0.5f);
        }

        isSpawn = false; 
    }
}

