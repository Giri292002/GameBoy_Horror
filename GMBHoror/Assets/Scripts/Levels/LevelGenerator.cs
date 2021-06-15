using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] Levels;

    private static GameObject[] _levels;
    private static GameObject _currentLevel = null;


    private GameObject _player;

    private Pathfinding.GridGraph graph;
    private GameObject LevelToSpawn;

    private StartPoints.Directions exitDirection; //Provided by player when exiting

    [SerializeField]
    private StartPoints[] startPoints;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _levels = Levels;
        CreateNewLevel();
    }

    public void CreateNewLevel(StartPoints.Directions exitDirection = StartPoints.Directions.north)
    {
        int i = Random.Range(0, (_levels.Length));

        //Setup a level for the first time

        if (_currentLevel == null)
        {
            var FirstLevelToSpawn = _levels[0];
            var firstinstance = GameObject.Instantiate(FirstLevelToSpawn, Vector3.zero, Quaternion.identity);
            graph = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graph);
            _currentLevel = firstinstance;
            SetPlayerPosition(exitDirection);
            return;
        }

        //Setup a New Level if not first
        //TOOD: Add Fade Out

        Destroy(_currentLevel);
        var LevelToSpawn = _levels[i];
        if (LevelToSpawn == _currentLevel)
        {
            i = Random.Range(0, (_levels.Length));
            LevelToSpawn = _levels[i];
        }
        var clone = GameObject.Instantiate(LevelToSpawn, Vector3.zero, Quaternion.identity);
        graph = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graph);
        _currentLevel = clone;

        SetPlayerPosition(exitDirection);


    }

    private void SetPlayerPosition(StartPoints.Directions exitDirection = StartPoints.Directions.south)
    {
        startPoints = _currentLevel.GetComponent<Level>().StartPoints;

        switch (exitDirection)
        {
            case StartPoints.Directions.east:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.west) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;


            case StartPoints.Directions.west:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.east) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            case StartPoints.Directions.north:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.south) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            case StartPoints.Directions.south:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.north) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            default:
                Debug.LogError("NO ENTRY POINTS, TRY ATTACHING A START POINT TO THE LEVEL CHUNK");
                break;
        };
        SetExitDoor(exitDirection, startPoints);
    }

    private void SetExitDoor(StartPoints.Directions exitDirection, StartPoints[] startPoints)
    {

        var j = Random.Range(0, startPoints.Length);
        // Player's entry door should not be the exit door too
        if (startPoints[j].ExitDirection == exitDirection)
        {
            j = j + 1 > startPoints.Length ? j = 0 : j + 1; //Get the next available door
        }
        var door = startPoints[j]; //Get the door
        door.SetAsExitDoor();
    }
}
