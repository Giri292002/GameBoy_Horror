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
}
