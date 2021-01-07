using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeKey", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject, 0.1f);
        }
    }

}
