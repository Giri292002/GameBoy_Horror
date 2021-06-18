using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSequence : MonoBehaviour
{
    [SerializeField]
    GameObject Chunk_01;
    [SerializeField]
    GameObject Chunk_02;

    public bool LoadNextChunk;
    public bool EndTutorial;
    private bool _doOnce = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (LoadNextChunk)
        {
            Chunk_01.SetActive(true);
            Chunk_02.SetActive(false);
        }
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (_doOnce == false)
            {
                if (LoadNextChunk)
                {
                    Destroy(Chunk_01);
                    Chunk_02.SetActive(true);
                    _doOnce = true;
                    return;
                }
                if (EndTutorial)
                {
                    SceneManager.LoadScene("GboyExperimentScene");
                    _doOnce = true;
                    return;
                }
            }

        }
    }
}
