using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    public GameObject portal;
    public float howMuchKey;
    float key = 0;
    RandomThings randomThings;
    public Text ile;
    public Text howMuch;

    private void Update()
    {
        ile.text = key.ToString();
        howMuch.text = howMuchKey.ToString();

        if (howMuchKey == key)
        {
            randomThings = new RandomThings();
            randomThings.DropObjects(portal, 1);
        }
    }

    public void TakeKey(float value)
    {
        key += value;
    }

    
}
