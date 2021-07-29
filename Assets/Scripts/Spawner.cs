using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public GameObject Player;
    public GameObject GameController;
    public GameObject GameManager;

    private void Awake()
    {
        Spawn(0);
    }

    private void Start()
    {
        if (!GameObject.FindWithTag("GameController"))
            Instantiate(GameController, transform.position, Quaternion.identity);
        if (!GameObject.FindWithTag("GameManager"))
            Instantiate(GameManager, transform.position, Quaternion.identity);
    }

    public void Spawn(int enemyType)
    {
        Instantiate(Enemy[enemyType], transform.position, Quaternion.identity);
    }

    public void PlayerRespawn()
    {
        Instantiate(Player);
    }
}
