using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + 2, 0, -1);
    }
}
