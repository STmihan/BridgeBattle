using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private Player _player;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
