using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Key : MonoBehaviour
{
    AudioSource _source;
    SpriteRenderer _sprite;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
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
            _sprite.enabled = false;
            _source.Play();
            other.GetComponent<PlayerController>().hasKey = true;
            GameObject.FindObjectOfType<MasterUI>().SetKeyVisiblity(true);
            Invoke("DestroyObject", _source.clip.length);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
