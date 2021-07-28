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
    
    [Space]
    public GameObject StartUI;
    public GameObject Player;
    public GameObject Spawner;
    public GameObject StartFightTrigger;

    public GameObject Enemy;
    

    private void Start()
    {
        Time.timeScale = 0;
        StartUI.SetActive(true);
        Load();
        Player = GameObject.FindWithTag("Player");
        Spawner = GameObject.FindWithTag("Spawner");
        StartFightTrigger = GameObject.FindWithTag("FightPosition");
    }

    private void Update()
    {
        if (HighScore < Score) HighScore=Score;
        Enemy = GameObject.FindWithTag("Enemy");
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
        Player player = Player.GetComponent<Player>();
        Enemy enemy = Enemy.GetComponent<Enemy>();
        MaxHpPlayer = player.maxHp;
        HpPlayer = player.maxHp;
        MaxHpEnemy = enemy.maxHp;
        HpEnemy = enemy.maxHp;
        Score = 0;
    }
}
