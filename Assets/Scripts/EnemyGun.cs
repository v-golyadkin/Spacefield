using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    void Start()
    {
        Invoke("Shoot", 1f);
    }

    private void Shoot()
    {
        GameObject playerShip = GameObject.Find("Player");

        SoundManager.instance.PlaySound2D("Enemy Shoot");

        if (playerShip != null) 
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
