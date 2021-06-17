using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableSpace : MonoBehaviour
{

    [SerializeField]
    BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector2 GetARandomSpawnablePosition()
    {
        Vector2 colliderPos = new Vector2(_collider.transform.position.x, _collider.transform.position.y) + _collider.offset;
        float randomPosX = Random.Range(colliderPos.x - _collider.size.x / 2, colliderPos.x + _collider.size.x / 2);
        float randomPosY = Random.Range(colliderPos.y - _collider.size.y / 2, colliderPos.y + _collider.size.y / 2);
        return new Vector2(randomPosX, randomPosY);
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    // void OnDrawGizmosSelected()
    // {
    //     var collider = GetComponent<BoxCollider2D>();
    //     var pos = GetARandomSpawnablePosition(collider);
    //     Gizmos.DrawSphere(pos, 0.2f);
    // }
}
