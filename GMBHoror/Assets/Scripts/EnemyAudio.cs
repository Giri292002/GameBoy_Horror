using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    GameObject _player;
    AudioSource _audio;
    [SerializeField]
    private float _distanceOffset = 2;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        var distance = GetDistanceFromPlayer();

        _audio.volume = Mathf.Lerp(1, 0, distance);
    }

    private float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        return distance / _distanceOffset;
    }
}
