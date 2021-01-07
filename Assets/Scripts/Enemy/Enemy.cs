using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    //zycie i pasek
    public float enemyMaxHealth = 50f;
    public Slider enemyHealthBar;
    public bool isBomb;
    public GameObject player;
    float currentHealth;

    // zmienne śmierci
    readonly float wlkCzastki = 0.2f;
    readonly int iloscWRzedzie = 5;
    float iloscCzastek;
    Vector3 pozycjaStartowa;
    public float silaEksplozji = 50f;
    public float promienEksplozji = 4f;
    readonly float jakWysokoEksplozja = 0.5f;
    GameObject ticTac;   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        iloscCzastek = wlkCzastki * iloscWRzedzie / 2;
        pozycjaStartowa = new Vector3(iloscCzastek, iloscCzastek, iloscCzastek);
    }

    private void Awake()
    {
        currentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 2 && isBomb)
        {
            Explosion();
        }

        enemyHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Explosion();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Explosion()    
    {        
        //deaktywacja roli obiektu
        gameObject.SetActive(false);

         //3 razy pętla, aby "pokroić" obiekt
        for (int x = 0; x < iloscWRzedzie; x++)
        {
            for (int y = 0; y < iloscWRzedzie; y++)
            {
                for (int z = 0; z < iloscWRzedzie; z++)
                {
                    PokrojObiekt(x, y, z);
                }
            }
        }

        //nadanie pozycji eksplozji
        Vector3 explosionPos = transform.position;

        //przypisanie obiektów do eksplozji
        Collider[] colliders = Physics.OverlapSphere(explosionPos, promienEksplozji);

        //dodanie wszystkim elementom tablicy "siły eksplozji"
        foreach (Collider hit in colliders)
        {
            //dodanie komponentu rb
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                int damage = Random.Range(10, 15);
                //parametry esplozji (siła, poz. wyjściowa, promien, kierunek)
                rb.AddExplosionForce(silaEksplozji, transform.position, promienEksplozji, jakWysokoEksplozja);
                rb.SendMessage("TakeDamage", (float)damage, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
 
    void PokrojObiekt(int x, int y, int z)
    {
        //tworzenie obiektu
        if (isBomb)
        {
            ticTac = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
        }
        else
        {
            ticTac = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
                
        //ustalanie pozycji i skali
        ticTac.transform.position = transform.position + new Vector3(wlkCzastki * x, wlkCzastki * y, wlkCzastki * z) - pozycjaStartowa;
        ticTac.transform.localScale = new Vector3(wlkCzastki, wlkCzastki, wlkCzastki);

        //dodawanie masy i rb
        ticTac.AddComponent<Rigidbody>();
        ticTac.GetComponent<Rigidbody>().mass = wlkCzastki;

        Destroy(ticTac, 5f);
    }    
}
