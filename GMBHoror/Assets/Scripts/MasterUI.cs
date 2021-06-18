using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterUI : MonoBehaviour
{
    [SerializeField]
    Animation GameOverAnim = null;
    public GameObject GameOverScreen = null;
    public Text GameOverScore;

    public GameObject Key = null;
    public Text Score = null;
    private int scoreNumber;

    public GameObject Credits;



    public void GoToLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        GameOverScore.text = "SCORE: " + scoreNumber;
        GameOverScreen.SetActive(true);
        GameOverAnim.Play();
    }

    public void SetKeyVisiblity(bool active)
    {
        Key.SetActive(active);
    }

    public void SetScore(int score)
    {
        scoreNumber = score;
        Score.text = "ROOMS CLEARED: " + score;
    }

    public void ShowCredits(bool visible)
    {
        Credits.SetActive(visible);
    }
}
