using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public float fireRadius = 25f;

    public GameObject bulletSpawnPoint;
    public GameObject enemyBullet;
    private Transform bulletSpawned;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= fireRadius)
        {
            EnemyShot();
        }
    }
    void EnemyShot()
    {
        Ray playerPos = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(playerPos, out RaycastHit hitPlayer, fireRadius))
        {
            if (hitPlayer.transform.CompareTag("Player"))
            {
                bulletSpawned = Instantiate(enemyBullet.transform, bulletSpawnPoint.transform.position, transform.rotation);
                bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;
            }
        }
    }
}
