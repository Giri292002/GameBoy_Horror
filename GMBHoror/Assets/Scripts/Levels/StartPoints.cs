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
    private float _spawnOffset = 0.2f;

    [SerializeField]
    private Sprite _openDoorSprite;
    [SerializeField]
    private BoxCollider2D _collider;

    public Directions ExitDirection;

    [SerializeField]
    //USED BY OTHERS THAT HAS THE OFFSETED VALUE
    public Vector2 WorldPos
    {
        get
        {
            switch (ExitDirection)
            {
                case Directions.north:
                    Debug.Log("EXITED AT NORTH, PUTTING PLAYER AT SOUTH ENTRANCE");
                    SpawnPosition = new Vector2(transform.position.x, transform.position.y - _spawnOffset);
                    break;
                case Directions.south:
                    Debug.Log("EXITED AT SOUTH, PUTTING PLAYER AT NORTH ENTRANCE");
                    SpawnPosition = new Vector2(transform.position.x, transform.position.y + _spawnOffset);
                    break;

                case Directions.east:
                    Debug.Log("EXITED AT EAST, PUTTING PLAYER AT WEST ENTRANCE");
                    SpawnPosition = new Vector2(transform.position.x - _spawnOffset, transform.position.y);
                    break;
                case Directions.west:
                    Debug.Log("EXITED AT WEST, PUTTING PLAYER AT EAST ENTRANCE");
                    SpawnPosition = new Vector2(transform.position.x + _spawnOffset, transform.position.y);
                    break;
                default:
                    SpawnPosition = Vector2.zero;
                    break;
            }
            return SpawnPosition;
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

        if (other.gameObject.GetComponent<PlayerController>() && bIsExitDoor && other.gameObject.GetComponent<PlayerController>().hasKey)
        {
            _levelGenerator.CreateNewLevel(ExitDirection);
        }
    }

    public void SetAsExitDoor()
    {
        bIsExitDoor = true;
    }

    //Called when the player has key
    public void ActivateExitDoor()
    {
        _sprite.sprite = _openDoorSprite;
        _collider.isTrigger = true;
    }

    public enum Directions
    {
        north,
        west,
        east,
        south
    }
}
