using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterUI : MonoBehaviour
{
    [SerializeField]
    Animation GameOverAnim = null;
    public GameObject GameOverScreen = null;

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
        GameOverScreen.SetActive(true);
        GameOverAnim.Play();
        //TODO: Get Score and Put it here
    }
}
