using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public float from, to;
    readonly float destroyAfter = 0.5f;
    float currentTime = 0;
    Vector3 newPosition;
    Vector3 oldPosition;
    bool hasHit = false;
    
    IEnumerator Start()
    {
        newPosition = transform.position;
        oldPosition = newPosition;

        while (currentTime < destroyAfter && !hasHit)
        {
            Vector3 velocity = transform.forward * speed;
            newPosition += velocity * Time.deltaTime;

            Vector3 direction = newPosition - oldPosition;
            float distance = direction.magnitude;

            // sprawdzanie czy coś nie uderzylo po drodze
            if (Physics.Raycast(oldPosition, direction, out RaycastHit hit, distance))
            {
                float damage = Random.Range(from, to);
                if (hit.rigidbody != null)
                {
                    hit.collider.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                }
                newPosition = hit.point; //przyjeto nowa pozycje
                StartCoroutine(DestroyBullet());
            }

            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();

            transform.position = newPosition;
            oldPosition = newPosition;
        }

        if (!hasHit)
        {
            StartCoroutine(DestroyBullet());
        }
    }
    IEnumerator DestroyBullet()
    {
        hasHit = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
