using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoints : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _sprite;

    private Vector2 SpawnPosition = Vector2.zero;

    [SerializeField]
    private float _spawnOffset = 0.5f;

    public Directions ExitDirection;

    [SerializeField]
    //USED BY OTHERS THAT HAS THE OFFSETED VALUE
    public Vector2 WorldPos
    {
        get
        {
            return new Vector2(SpawnPosition.x, SpawnPosition.y);
        }
    }

    private LevelGenerator _levelGenerator;


    [Header("DONT TOUCH")]
    public bool bIsExitDoor; //SET BY LEVEL GENERATOR

    // Start is called before the first frame update
    void Start()
    {
        SetupSpawnPosition();
        _levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        Debug.Log($"Start Position of: {ExitDirection} is - {WorldPos}");
        _sprite.enabled = false;
    }

    private void SetupSpawnPosition()
    {
        switch (ExitDirection)
        {
            case Directions.north:
                SpawnPosition = new Vector2(transform.position.x, transform.position.y - _spawnOffset);
                break;
            case Directions.south:
                SpawnPosition = new Vector2(transform.position.x, transform.position.y + _spawnOffset);
                break;
            case Directions.east:
                SpawnPosition = new Vector2(transform.position.x - _spawnOffset, transform.position.y);
                break;
            case Directions.west:
                SpawnPosition = new Vector2(transform.position.x + _spawnOffset, transform.position.y);
                break;
            default:
                SpawnPosition = Vector2.zero;
                break;
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Exited: {ExitDirection}");
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _levelGenerator.CreateNewLevel();
            Debug.Log($"Exited: {ExitDirection}");

        }
    }

    public enum Directions
    {
        north,
        west,
        east,
        south
    }
}
