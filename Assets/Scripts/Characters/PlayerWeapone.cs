using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapone : MonoBehaviour
{
    public GameObject GameObject;
    private Player player;

    private void Start()
    {
        player = GameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && player.PlayerState == PlayerState.Fight)
        {
            StartCoroutine(player.Enemy.TakeDamage(player.damage));
        }
    }
}
