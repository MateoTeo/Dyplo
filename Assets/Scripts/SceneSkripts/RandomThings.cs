using UnityEngine;

public class RandomThings : MonoBehaviour
{   
    public GameObject apteczka;
    public int ilApteczka;
    public GameObject enemyShooter;
    public int ilEnemyS;
    public GameObject enemyBomb;
    public int ilEnemyB;
    public GameObject key;
    public int ilKey;
    public GameObject Player;
    public float xPos;
    public float zPos;
    public float fromX;
    public float toX;
    public float fromZ;
    public float toZ;


    void Awake()
    {   
        DropObjects(Player, 1);
        DropObjects(apteczka, ilApteczka);
        DropObjects(key, ilKey);
        DropObjects(enemyShooter, ilEnemyS);
        DropObjects(enemyBomb, ilEnemyB);
    }

    private void Update()
    {
        ReloadGameObject("FirstKit", apteczka, ilApteczka);
        ReloadGameObject("EnemyBomb", enemyBomb, ilEnemyB);
        ReloadGameObject("EnemyShooter", enemyShooter, ilEnemyS);
    }
    void ReloadGameObject(string tag, GameObject reloadGameObject, float count)
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag(tag);

        if (gameObject.Length < count)
        {
            DropObjects(reloadGameObject, 1);
        }
    }
    public float GetNewPosition(float from, float to)
    {
        return Random.Range(from, to);
    }
    public void DropObjects(GameObject gameObject, int count)
    {
        for (int i = 0; i < count; i++)
        {

            xPos = GetNewPosition(fromX, toX);
            zPos = GetNewPosition(fromZ, toZ);

            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(xPos, 2, zPos), 1f);
            

            if (hitColliders.Length == 0)
            {
                Instantiate(gameObject, new Vector3(xPos,2,zPos), Quaternion.identity); 
            }
            else
            {
                DropObjects(gameObject, 0);
            }
                    
        }
    }
}
