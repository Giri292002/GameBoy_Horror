using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2 Pos
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }

    public bool IsTrapRoom; // Set by level generator
    private bool _onceDone = false; // Set to true if the trap was activated atleast once, preventing the trap to be called again

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && IsTrapRoom && !_onceDone)
        {
            var enemies = GameObject.FindObjectsOfType<AIController>();
            foreach (var item in enemies)
            {
                item._aiFSM.SetTrigger("Chase");
            }
        }
    }
}
