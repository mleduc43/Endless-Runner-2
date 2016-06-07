using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float power = 60;
    public int jumpHeight = 1000;
    public bool isFalling = false;

    public int highScore;

    public bool canDoubleJump;
    public bool jumpOne;
    public bool jumpTwo;

    public GameObject bloodParticles;
    public AudioClip playerHitSound;
    private AudioSource audioSource;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        jumpOne = false;
        jumpTwo = false;
        canDoubleJump = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckJump();
    }

    //Physics Update
    void FixedUpdate()
    {
        //Movement
        transform.Translate(Vector2.right * power * Time.deltaTime);

        if (jumpOne)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
            jumpOne = false;
        }

        if (jumpTwo)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
            jumpTwo = false;
        }
    }

    private void CheckJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && !isFalling) || (Input.GetTouch(0).phase == TouchPhase.Began && !isFalling))
        {
            jumpOne = true;
            canDoubleJump = true;
            isFalling = true;
            return;
        }

        if ((Input.GetKeyDown(KeyCode.Space) && isFalling && canDoubleJump) || (Input.GetTouch(0).phase == TouchPhase.Began && isFalling && canDoubleJump))
        {
            jumpTwo = true;
            canDoubleJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isFalling = false;
            canDoubleJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Death")
        {
            audioSource.PlayOneShot(playerHitSound, 0.7f);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>().TakeDamage(1);
            ShowBlood();
        }
    }

    private void ShowBlood()
    {
        GameObject childObject = Instantiate(bloodParticles) as GameObject;
        childObject.transform.SetParent(this.transform, false);
        Destroy(childObject, 2);
    }
}
