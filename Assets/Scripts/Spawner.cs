using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public GameObject Player;
    public GameManager GameManager;
    
    public void Spawn(int enemyType)
    {
        var enemy = Instantiate(Enemy[enemyType], transform.position, Quaternion.identity);
        GameManager.Enemy = enemy.GetComponent<Enemy>();
        GameManager.Enemy.maxHp = GameManager.maxHpNextEnemy;
        GameManager.Enemy.HpEnemy = GameManager.maxHpNextEnemy;
        GameManager.Enemy.damage = GameManager.damageNextEnemy;
        GameManager.Enemy.attackSpeed = GameManager.attackSpeedNextEnemy;
    }

    public void PlayerRespawn()
    {
        Instantiate(Player);
    }
}
