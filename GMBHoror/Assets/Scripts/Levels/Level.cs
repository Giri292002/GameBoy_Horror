using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public StartPoints[] StartPoints;

    public List<Room> Rooms;

    public SpawnableSpace SpawnableSpace;

    public int z;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        StartPoints = GetComponentsInChildren<StartPoints>();

        var rooms = GetComponentsInChildren<Room>();

        foreach (var item in rooms)
        {
            Rooms.Add(item);
        }

        SpawnableSpace = GetComponentInChildren<SpawnableSpace>();
    }
}
