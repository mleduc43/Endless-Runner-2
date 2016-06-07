using UnityEngine;
using System.Collections;
using System.Linq;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] unlockedPowerUps;
    public GameObject[] magnets;
    public GameObject[] coinX2s;
    public GameObject[] PUSP;

    void Start()
    {
        magnets = GameObject.FindGameObjectsWithTag("Magnet");
        coinX2s = GameObject.FindGameObjectsWithTag("CoinX2");

        unlockedPowerUps = magnets.Concat(coinX2s).ToArray();
    }

    public void PUSPCheck()
    {
        PUSP = GameObject.FindGameObjectsWithTag("PUSP");

        foreach (GameObject powerUpSpawnPoint in PUSP)
        {
            powerUpSpawnPoint.gameObject.tag = "Untagged";
            if (unlockedPowerUps.Length > 0)
                Instantiate(unlockedPowerUps[Random.Range(0, unlockedPowerUps.Length)], powerUpSpawnPoint.transform.position, powerUpSpawnPoint.transform.rotation);
        }
    }
}
