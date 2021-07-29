using UnityEngine;

public class GameController : MonoBehaviour
{
    [Space][Header("Enemy")]
    public int maxHpEnemy;
    public float attackSpeedEnemy;
    public int damageEnemy;
    public float spawnSpeedEnemy;

    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    int counter = 0;
    public void onEnemyDeath()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ScoreUp();
        GameManager.Spawner.GetComponent<Spawner>().Spawn(0);
        counter++;
        if (counter == 5) 
        {
            EnemyPowerUp();
            counter = 0;
        }
    }

    private void EnemyPowerUp()
    {
        Debug.Log("Enemy power up");
        maxHpEnemy = Mathf.RoundToInt(maxHpEnemy * 1.1f);
        attackSpeedEnemy *= 1.1f;
        damageEnemy = Mathf.RoundToInt(damageEnemy * 1.1f);
    }
}
