using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
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

    public AudioSource _source;

    public void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }

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
