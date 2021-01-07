using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : Enemy
{
    //zasieg wzroku ai i pamieć pozycji gracza
    bool graczWOstatniejPozycji = false;
    float katWidzenia = 140f;
    float zasiegGonitwy = 30f;
    float timerObrotu;
    bool AIZapamietaloGracza = false;
    float pamiecStartCzasu = 10f;
    float czasPamieci;

    //AI "Słuch"
    Vector3 pozycjaStrzalu;
    bool czySlyszalGracza = false;
    public float dystansSlyszeniaStrzalu = 35f;
    float predkoscObrotu = 2f;
    bool mozliwyObrot = false;
    float czasObrotu = 2f;

    //randomowe patrolowanie po tablicy pkt
    float xPos;
    float zPos;
    public float odX;
    public float doX;
    public float odZ;
    public float doZ;
    Vector3 pktPos;
    NavMeshAgent nav;
    public float minDystansDoPkt = 2f;

    //AI odskok
    public float dystansDo = 2f; //jak blisko może podejsc
    float odskok;
    public float czas_min;
    public float czas_max;
    public Transform odskokLewo;
    public Transform odskokPrawo;
    int losowyKierunekOdskoku;
    float czasCzekania;
    float czasStartowy = 1f;

    //pogoń
    public float predkoscObrotWStroneGracza = 15f;
    float losowyCzas;


    private void Start()
    {
        GetNewPosition();
        nav.isStopped = false;
        czasCzekania = czasStartowy;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
    }


    private void Update()
    {
        //mierzymy dystans od enemy do gracza
        float dystans = Vector3.Distance(transform.position, player.transform.position);

        if (dystans <= zasiegGonitwy)
        {
            SprawdzOstatniaPozycje();
        }

        if (nav.isActiveAndEnabled)
        {
            if (graczWOstatniejPozycji == false && AIZapamietaloGracza == false && czySlyszalGracza == false)
            {
                Patrol();
                SprawdzHalas();
                StopCoroutine(PamiecAI());
            }
            else if (czySlyszalGracza == true && graczWOstatniejPozycji == false && AIZapamietaloGracza == false)
            {
                mozliwyObrot = true;
                IdzDoPozycjiHalasu();
            }
            else if (graczWOstatniejPozycji == true) //kiedy enemy widzi obiekt player
            {
                AIZapamietaloGracza = true;
                ObrotKuGraczowi();
                PogonZaGraczem();
            }
            else if (AIZapamietaloGracza == true && graczWOstatniejPozycji == false)
            {
                PogonZaGraczem();
                StartCoroutine(PamiecAI());
            }
        }
    }

    private void LateUpdate()
    {
        if (AIZapamietaloGracza == true && graczWOstatniejPozycji == false)
        {
            dystansDo = 1f;
        }
        else
        {
            dystansDo = 10f;
        }
    }
    private void SprawdzHalas() //Funkcja zapisuje miejsce z ktorego oddano strzal do enemy
    {
        float dystans = Vector3.Distance(transform.position, player.transform.position); //pobieramy dystans

        if (dystans <= dystansSlyszeniaStrzalu) //jesli jest w zakresie "słyszalności"
        {
            if (Input.GetButton("Fire1"))
            {
                pozycjaStrzalu = player.transform.position; //przypisz pozycje gracza do zmiennej noisePosition
                czySlyszalGracza = true; //przypisz ze enemy slyszal gracza
            }
            else
            {
                czySlyszalGracza = false;
                mozliwyObrot = false;
            }
        }
    }
    private void IdzDoPozycjiHalasu() //przejdz do pozycji z ktorej oddano strzal
    {
        nav.SetDestination(pozycjaStrzalu);

        //jesli odleglosc jest wieksza jak min dystansowe i moze szukac
        if (Vector3.Distance(transform.position, pozycjaStrzalu) <= 5f && mozliwyObrot == true)
        {
            //wlacz timer szukania
            timerObrotu += Time.deltaTime;

            transform.Rotate(Vector3.up * predkoscObrotu, Space.World); //rotacja  obiektem, obracamy w stosunku do swiata gry

            if (timerObrotu >= czasObrotu) //jesli czas się skonczyl
            {
                mozliwyObrot = false; //nie moze szukac
                czySlyszalGracza = false; //przestal "slyszec" gracza
                timerObrotu = 0f; //zeruj licznik
            }
        }
    }
    IEnumerator PamiecAI()
    {
        czasPamieci = 0; //resetowanie czasu pamieci

        while (czasPamieci < pamiecStartCzasu)
        {
            czasPamieci += Time.deltaTime; //wlaczamy licznik
            AIZapamietaloGracza = true; //ustawiamy ze ai pamieta pozycje playera
            yield return null;
        }

        czySlyszalGracza = false; //ai nie slyszy playera
        AIZapamietaloGracza = false; //ai nie pamieta pozycji playera
    }
    private void SprawdzOstatniaPozycje()
    {
        Vector3 kierunek = player.transform.position - transform.position; //wektor kierunkowy

        float kat = Vector3.Angle(kierunek, transform.forward); //okreslanie kątu wobec gracza

        if (kat < katWidzenia * 0.5f)
        {
            //jesli na drodze po miedzy enemy a kierunkiem gdzie byl player, w zasiegu losRadius, bedzie obiekt o tagu Player, w za
            if (Physics.Raycast(transform.position, kierunek.normalized, out RaycastHit hit, zasiegGonitwy))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    graczWOstatniejPozycji = true; //enemy znalazl gracza
                    AIZapamietaloGracza = true; //zapamietal pozycje
                }
                else
                {
                    graczWOstatniejPozycji = false; //player zgubiony
                }
            }
        }
    }
    void GetNewPosition()
    {
        xPos = Random.Range(odX, doX);
        zPos = Random.Range(odZ, doZ);
        pktPos = new Vector3(xPos, 1, zPos);
    }
    private void Patrol()
    {        
        nav.SetDestination(pktPos);        

        //jesli dystans pomiedzy enemy a pkt  jest mniejszy niz min dystans
        if (Vector3.Distance(transform.position, pktPos) < minDystansDoPkt)
        {
            if (czasCzekania <= 0)
            {
                //nadaj losowo nowy pkt z tablicy i wyzeruj zegar
                czasCzekania = czasStartowy;
                GetNewPosition();
            }
            else
            {
                //odliczaj do tylu
                czasCzekania -= Time.deltaTime;
            }
        }
    }
    private void PogonZaGraczem()
    {
        //obliczanie dystansu pomiedzy enemy a player
        float dystans = Vector3.Distance(transform.position, player.transform.position);

        //jesli dystans jest mniejszy niz pole widzenia i wiekszy niz dystans do gracza
        if (dystans <= zasiegGonitwy && dystans > dystansDo && isBomb)
        {
            nav.SetDestination(player.transform.position); //podąrzaj za graczem
        }
        //jeśli navMeshAgent jest aktywny i dystans do gracza jest mniej niz minimum
        else if (nav.isActiveAndEnabled && dystans <= dystansDo && !isBomb) 
        {
            losowyKierunekOdskoku = Random.Range(0, 1); //losuj liczbe

            //Debug.Log($"Wylosowano: {randomStrafeDirection}");
            losowyCzas = Random.Range(czas_min, czas_max); // losuj czas

            //jesli czas czekania jest mniejszy niz 0
            if (odskok <= 0)
            {
                if (losowyKierunekOdskoku == 0) //jesli sie wylosowalo 0
                {
                    nav.SetDestination(odskokLewo.position);//przesun sie na pozycje transform obiektu w lewo
                }
                else if (losowyKierunekOdskoku == 1)
                {
                    nav.SetDestination(odskokPrawo.position);
                }
                odskok = losowyCzas; //przypisz wylosowany czas
            }
            else //jesli nie - odliczaj do tylu
            {
                odskok -= Time.deltaTime;
            }
        }
    }
    private void ObrotKuGraczowi()
    {
        Vector3 kierunek = (player.transform.position - transform.position).normalized;

        Quaternion rotacjaObrotu = Quaternion.LookRotation(new Vector3(kierunek.x, 0, kierunek.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacjaObrotu, Time.deltaTime * predkoscObrotWStroneGracza);
    }
}
