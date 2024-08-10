using System.Collections;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField] private AutoBullet bulletPrefab;
    [SerializeField] private float velocidadSpawn = 0.5f;
    [SerializeField] private float bullettime = 5f;
    [SerializeField] private int bulletShot = 3;

    private Transform player;
    private Coroutine shootingC;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        shootingC = StartCoroutine(ShootCont());
    }

    private void OnDisable()
    {
        if (shootingC != null)
        {
            StopCoroutine(shootingC);
        }
    }

    private IEnumerator ShootCont()
    {
        while (true)
        {
            yield return StartCoroutine(Shoot());
            yield return new WaitForSeconds(velocidadSpawn);
        }
    }

    private IEnumerator Shoot()
    {
        if (player != null)
        {
            for (int i = 0; i < bulletShot; i++)
            {
                AutoBullet bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Destroy(bulletInstance.gameObject, bullettime);

                yield return new WaitForSeconds(velocidadSpawn / bulletShot);
            }
        }
    }
}


