using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapone : MonoBehaviour
{
    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = GameManager.Player.GetComponent<Player>();
        var enemy = GameManager.Enemy.GetComponent<Enemy>();
        if (other.CompareTag("PlayerTrigger"))
        {
            Debug.Log("Enemy attacked");
            player.TakeDamage(enemy.damage);
        }
    }
}