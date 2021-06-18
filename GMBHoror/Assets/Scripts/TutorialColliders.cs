using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialColliders : MonoBehaviour
{

    [SerializeField]
    private Image _image;

    [SerializeField]
    private string Message;

    private bool _doOnce = false;

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
                _image.GetComponent<Animator>().SetTrigger("Appear");
                _image.GetComponentInChildren<TextGenerator>().PlayTextAnimation(Message, other.GetComponentInChildren<PlayerController>());
                _doOnce = true;
            }
        }
    }
}
