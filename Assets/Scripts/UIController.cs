using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject MainMenu;

    public Text HIGHSCORE;
    public int highScore;
    public Text SCORE;
    public int score = 0;

    public Text COINS;
    public int coins = 0;
    public Text BANK;
    public int bank = 0;

    public GameObject pnlYouDied;

    private int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    public bool Playing;

    // Use this for initialization
    void Start()
    {
        Playing = false;

        //Hide health
        health3.SetActive(false);
        health2.SetActive(false);
        health1.SetActive(false);

        pnlYouDied.SetActive(false);
        MainMenu.SetActive(true);
        Time.timeScale = 0.0f;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bank = PlayerPrefs.GetInt("CoinBank", 0);
        HIGHSCORE.text = string.Format("HighScore {0}", highScore.ToString("000.##"));
        BANK.text = string.Format("BANK {0}", bank.ToString("000.##"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            CheckHealth();
        }
        else
        {
            COINS.text = "";
            SCORE.text = "";
        }
    }

    private void CheckHealth()
    {
        switch (health)
        {
            case 3:
                health3.SetActive(true);
                health2.SetActive(true);
                health1.SetActive(true);
                break;
            case 2:
                health3.SetActive(false);
                health2.SetActive(true);
                health1.SetActive(true);
                break;
            case 1:
                health3.SetActive(false);
                health2.SetActive(false);
                health1.SetActive(true);
                break;
            case 0:
                health3.SetActive(false);
                health2.SetActive(false);
                health1.SetActive(false);
                pnlYouDied.SetActive(true);
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
    }

    public void ScorePlus(int newScore)
    {
        score += newScore;
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        SCORE.text = string.Format("SCORE {0}", score.ToString("000.##"));
    }

    public void CoinPlus(int newCoin)
    {
        coins += newCoin;
        bank += newCoin;
        PlayerPrefs.SetInt("CoinBank", bank);

        COINS.text = string.Format("COINS {0}", coins.ToString("000.##"));
    }

    public void OnClick_StartButton()
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Playing = true;

        SCORE.text = string.Format("SCORE {0}", score.ToString("000.##"));
        COINS.text = string.Format("COINS {0}", coins.ToString("000.##"));
    }

    public void OnClick_QuitButton()
    {
        Application.Quit();
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void OnClick_Restart()
    {
        Time.timeScale = 1.0f;
        health = 3;
        SceneManager.LoadScene(0); //Reload the level
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }
}
