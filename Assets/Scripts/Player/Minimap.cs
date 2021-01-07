using UnityEngine;

public class Minimap : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    private void LateUpdate()
    {
        //przypisanie pozycji gracza kamerze
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //obracanie kamery zgodnie z obrotem gracza
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
