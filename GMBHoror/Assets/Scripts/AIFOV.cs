using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFOV : MonoBehaviour
{

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterController>())
        {
            Debug.Log("I SEE THE PLAYER");
        }
    }
}
