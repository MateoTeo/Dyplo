using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;

    public bool isOn;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.intensity = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn == false)
            {
                On();
            }
            else
            {
                Off();
            }
        }
    }

    void On()
    {
        flashlight.intensity = 2.5f;
        isOn = true;
    }
    void Off()
    {
        flashlight.intensity = 0f;
        isOn = false;
    }
}

