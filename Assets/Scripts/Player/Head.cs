using UnityEngine;

public class Head : MonoBehaviour
{
    public float speed = 0.18f;
    public float height = 0.2f;
    public float midpoint = 1.8f;
    bool isHead = true;


    float timer = 0.0f;

    void Update()
    {
        float sin = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 vector = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            sin = Mathf.Sin(timer);
            timer += speed;

            if (timer > Mathf.PI * 2)
            {
                timer -= (Mathf.PI * 2);
            }
        }
        if (sin != 0)
        {
            float changeSin = sin * height;
            float angle = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            angle = Mathf.Clamp(angle, 0.0f, 1.0f);
            changeSin = angle * changeSin;

            if (isHead == true)
            {
                vector.y = midpoint + changeSin;
            }
            else if (isHead == false)
            {
                vector.x = changeSin;
            }
        }
        else
        {
            if (isHead == true)
            {
                vector.y = midpoint;
            }
            else if (isHead == false)
            {
                vector.x = 0;
            }
        }
        transform.localPosition = vector;
    }
}

