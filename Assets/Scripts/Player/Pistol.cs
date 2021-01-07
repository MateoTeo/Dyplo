using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Player
{
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    private Transform bulletSpawned;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isDead)
        {    
            bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
            bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;
        }
    }
}