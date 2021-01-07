using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstKit : MonoBehaviour
{
    public float healthPlus = 50;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeFirstKit", healthPlus, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
