using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene II");
        }
    }
}