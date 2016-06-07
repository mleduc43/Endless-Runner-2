using UnityEngine;
using System.Collections;

public class BlockSpawn : MonoBehaviour 
{
    public GameObject[] blocks;
    public Transform blockSpawnerPos;
    public int blockCount = 0;
    public float newPos = 5.0f;

    private int randomNum;
    private float waitTime = 0.5f;
    private GameObject block;

	// Use this for initialization
	void Start() 
    {
        Block();
	}

    public void Block()
    {
        block = Instantiate(blocks[Random.Range(0, blocks.Length)], blockSpawnerPos.position, Quaternion.identity) as GameObject;
        Vector3 temp = blockSpawnerPos.position;
        temp.y = 0;
        temp.x += newPos;
        temp.z = 0;
        blockSpawnerPos.position = temp;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PowerUpSpawn>().PUSPCheck();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Coin>().LevelCheck();
        yield return new WaitForSeconds(waitTime);
        blockCount+= 1;
        Block();
    }
}
