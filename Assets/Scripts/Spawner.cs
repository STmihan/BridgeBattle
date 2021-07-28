using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemy;

    private void Start()
    {
        Spawn(0);
    }

    public void Spawn(int enemyType)
    {
        Instantiate(Enemy[enemyType], transform.position, Quaternion.identity);
    }
}
