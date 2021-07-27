using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MaxHpPlayer;
    public int HpPlayer;
    public int MaxHpEnemy;
    public int HpEnemy;
    public int Score;
    public int HighScore;

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (HighScore < Score) HighScore=Score;
    }

    public void ScoreUp()
    {
        Score++;
    }

    public void Save()
    {
        
    }

    public void Load()
    {
        
    }

    public void Restart()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Player enemy = GameObject.FindWithTag("Enemy").GetComponent<Player>();
        MaxHpPlayer = player.maxHp;
        HpPlayer = player.maxHp;
        MaxHpEnemy = enemy.maxHp;
        HpEnemy = enemy.maxHp;
        Score = 0;
    }
}
