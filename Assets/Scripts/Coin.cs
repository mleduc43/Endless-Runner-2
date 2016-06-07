using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public AudioClip coinGrabSound;
    public AudioClip powerUpSound;
    private AudioSource audioSource;

    public bool coinMagnet = false;
    public float timerCoinMagnet = 0.0f;

    public bool coinX2 = false;
    public float timerCoinX2 = 0.0f;
    public int coinX2Modifier = 1;

    public GameObject[] currentCoins;
    public GameObject[] magnets;
    public GameObject[] coinsX2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (coinMagnet)
        {
            timerCoinMagnet += 1 * Time.deltaTime;
            if (timerCoinMagnet > 10)
            {
                coinMagnet = false;
                timerCoinMagnet = 0.0f;
            }
        }

        if (coinX2)
        {
            timerCoinX2 += 1 * Time.deltaTime;
            if (timerCoinX2 > 10)
            {
                coinX2 = false;
                timerCoinX2 = 0.0f;
                coinX2Modifier = 1;
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject coin in currentCoins)
        {
            if (coin != null)
            {
                if (Vector3.Distance(coin.transform.position, player.transform.position) < 0.75)
                {
                    Destroy(coin.gameObject);
                    audioSource.PlayOneShot(coinGrabSound, 0.7f);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>().CoinPlus(1 * coinX2Modifier);
                }

                if (coinMagnet)
                {
                    if (Vector3.Distance(coin.transform.position, player.transform.position) < 5)
                    {
                        coin.transform.Translate((player.transform.position - coin.transform.position).normalized * 4 * Time.deltaTime, Space.World);
                    }
                }
            }
        }

        foreach (GameObject magnet in magnets)
        {
            if (magnet != null)
            {
                if (Vector3.Distance(magnet.transform.position, player.transform.position) < 0.5)
                {
                    Destroy(magnet.gameObject);
                    audioSource.PlayOneShot(powerUpSound, 0.7f);
                    coinMagnet = true;
                }
            }
        }

        foreach (GameObject cX2 in coinsX2)
        {
            if (cX2 != null)
            {
                if (Vector3.Distance(cX2.transform.position, player.transform.position) < 0.5)
                {
                    Destroy(cX2.gameObject);
                    audioSource.PlayOneShot(powerUpSound, 0.7f);
                    coinX2 = true;
                    coinX2Modifier = 2;
                }
            }
        }
    }

    public void LevelCheck()
    {
        currentCoins = GameObject.FindGameObjectsWithTag("Coin");
        magnets = GameObject.FindGameObjectsWithTag("Magnet");
        coinsX2 = GameObject.FindGameObjectsWithTag("CoinX2");
    }
}
