using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _enemy._enemyState == EnemyState.Fight)
        {
            _enemy._player.TakeDamage(_enemy.damage);
        }
    }
}