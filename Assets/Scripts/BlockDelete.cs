using UnityEngine;
using System.Collections;

public class BlockDelete : MonoBehaviour
{
    private float timeToLive = 2.0f;
    public GameObject UI;

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(Delete());

        if (UI == null)
        {
            UI = GameObject.FindGameObjectWithTag("GameController");
        }

        UI.GetComponent<UIController>().ScorePlus(1);
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
