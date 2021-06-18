using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject Key;
    public GameObject SafeZone;

    private GameObject[] _levels;
    private GameObject _currentLevel = null;
    public StartPoints _currentExitDoor = null;
    private Room _currentKeyRoom = null;
    private Room _currentTrapRoom = null;
    private GameObject _currentSafeZone = null;

    [SerializeField]
    private Animator _uiAnimator;
    private GameObject _player;
    private Pathfinding.GridGraph graph;
    private GameObject LevelToSpawn;
    private StartPoints.Directions exitDirection; //Provided by player when exiting
    private MasterUI InGameHUD;
    private int _score = 0;
    private AudioSource _source;

    [SerializeField]
    private StartPoints[] startPoints;

    [Header("DEBUG")]
    public bool isDebug = false;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();

        _player = GameObject.FindGameObjectWithTag("Player");

        InGameHUD = FindObjectOfType<MasterUI>();
        InGameHUD.SetKeyVisiblity(false);
        InGameHUD.SetScore(_score);


        _levels = Levels;
        if (isDebug == false)
            CreateNewLevel();
    }

    public void CreateNewLevel(StartPoints.Directions exitDirection = StartPoints.Directions.west)
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
        _uiAnimator.SetTrigger("PlayAnimation");
        _source.Play();

        Destroy(_currentLevel);
        Destroy(_currentSafeZone);

        _score++;
        InGameHUD.SetScore(_score);
        InGameHUD.SetKeyVisiblity(false);

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
                    // Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;

            case StartPoints.Directions.west:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.east) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    //Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            case StartPoints.Directions.north:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.south) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    // Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            case StartPoints.Directions.south:
                foreach (var item in startPoints)
                {
                    if (item.ExitDirection != StartPoints.Directions.north) continue;
                    _player.transform.position = new Vector3(item.WorldPos.x, item.WorldPos.y, 0f);
                    //Debug.Log($"Set Player positions as: {item.WorldPos} at {item.ExitDirection}");
                    break;
                }
                break;
            default:
                Debug.LogError("NO ENTRY POINTS, TRY ATTACHING A START POINT TO THE LEVEL CHUNK");
                break;
        };
        _player.GetComponent<PlayerController>().hasKey = false;
        SetExitDoor(exitDirection, startPoints);
        SetupRooms();
        SetupSafeZone();
    }

    private void SetExitDoor(StartPoints.Directions exitDirection, StartPoints[] startPoints)
    {

        var j = Random.Range(0, startPoints.Length);
        // Player's entry door should not be the exit door too
        if (startPoints[j].ExitDirection == exitDirection)
        {
            j = j + 1 >= startPoints.Length ? j = 0 : j + 1; //Get the next available door
        }
        var door = startPoints[j]; //Get the door
        _currentExitDoor = door;
        door.SetAsExitDoor();

    }

    private void SetupRooms()
    {
        // Set Key Room
        var rooms = _currentLevel.GetComponent<Level>().Rooms;
        List<Room> AvailableRooms = rooms;
        int i = Random.Range(0, AvailableRooms.Count);
        Instantiate(Key, new Vector3(rooms[i].Pos.x, rooms[i].Pos.y, 0), Quaternion.identity);
        _currentKeyRoom = rooms[i];
        AvailableRooms.Remove(_currentKeyRoom);

        //Set Trap Rooms
        i = Random.Range(0, AvailableRooms.Count);
        AvailableRooms[i].IsTrapRoom = true;
        _currentTrapRoom = AvailableRooms[i];
        AvailableRooms.Remove(_currentTrapRoom);

    }

    private void SetupSafeZone()
    {
        var space = _currentLevel.GetComponent<Level>().SpawnableSpace;
        var spawnPos = space.GetARandomSpawnablePosition();
        _currentSafeZone = GameObject.Instantiate(SafeZone, spawnPos, Quaternion.identity);
    }
}
