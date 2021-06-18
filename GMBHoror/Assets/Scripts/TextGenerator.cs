using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    public string Message;
    public Queue<char> BrokenDownString = new Queue<char>();

    [SerializeField]
    private Text _text;

    [SerializeField]
    private Image _image;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _text.text = "";
    }

    public void PlayTextAnimation(string message, PlayerController _player)
    {
        Message = message;
        _text.text = "";
        _player.canMove = false;
        StartCoroutine(PlayText(_player));
    }

    IEnumerator PlayText(PlayerController _player)
    {
        foreach (char c in Message)
        {
            _text.text += c;
            yield return new WaitForSeconds(0.050f);
        }

        yield return new WaitForSeconds(2.0f);

        _text.text = "";
        _player.canMove = true;
        _image.GetComponent<Animator>().SetTrigger("Appear");
    }
}
