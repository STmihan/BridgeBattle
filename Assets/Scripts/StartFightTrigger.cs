using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFightTrigger : MonoBehaviour
{

    public Player Player;
    void Start()
    {
        Player = Player.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.Enemy = other.gameObject.GetComponent<Enemy>();
        Player._fightState = Player.FightState.Fight;
    }
}
