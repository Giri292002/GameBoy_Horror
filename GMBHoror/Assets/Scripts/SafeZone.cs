using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    CircleCollider2D _collider;
    AudioSource _source;
    public bool _isStillIn;
    AIController[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _source = GetComponent<AudioSource>();
        enemies = GameObject.FindObjectsOfType<AIController>();
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
            _source.Play();
            _isStillIn = true;
            StartCoroutine(SetToPatrol());

        }
    }

    IEnumerator SetToPatrol()
    {
        while (_isStillIn)
        {
            foreach (var item in enemies)
            {
                if (item._aiState == AIController.AIStates.chase)
                {
                    item._aiFSM.SetTrigger("Patrol");
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _isStillIn = false;
        }
    }


}
