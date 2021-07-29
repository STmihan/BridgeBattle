using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Inspector fields
    public int MaxHpPlayer;
    public int HpPlayer;
    public int MaxHpEnemy;
    public int HpEnemy;
    public int Score;
    public int HighScore;
    public GameObject StartFightTrigger;
    #endregion

    #region Hide in inspector fields

    public Spawner Spawner;
    public Player Player;
    public Enemy Enemy;
    public bool isFight { get; set; }
    #endregion

    #region next enemy char
    public int maxHpNextEnemy { get; private set; }
    public float attackSpeedNextEnemy { get; private set; }
    public int damageNextEnemy { get; private set; }
    
    #endregion

    #region Save fields
    private Save sv = new Save();
    private string path;
    #endregion
    
    private void Start()
    {
        Spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        isFight = false;
        StartFightTrigger = GameObject.FindWithTag("FightPosition");
        Time.timeScale = 0;
        Load();
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        if (Enemy == null)
        {
            Enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        }
        maxHpNextEnemy = Enemy.maxHp;
        attackSpeedNextEnemy = Enemy.attackSpeed;
        damageNextEnemy = Enemy.damage;
    }

    private void Update()
    {

        if (HighScore < Score) HighScore=Score;
    }

    int counter = 0;
    public void onEnemyDeath()
    {
        Score++;
        Spawner.Spawn(0);
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
        maxHpNextEnemy = Mathf.RoundToInt(maxHpNextEnemy * 1.1f);
        attackSpeedNextEnemy *= 1.1f;
        damageNextEnemy = Mathf.RoundToInt(damageNextEnemy * 1.1f);
    }
    
    #region Save/Load
    public void Save()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        sv.MaxHpPlayer = Player.maxHp;
        sv.HpPlayer = Player.Hp;
        sv.MaxHpEnemy = Enemy.maxHp;
        sv.HpEnemy = Enemy.maxHp;
        sv.Score = Score;
        sv.HighScore = HighScore;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }

    public void Load()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            Player.maxHp = sv.MaxHpPlayer;
            Player.Hp = sv.HpPlayer;
            Enemy.maxHp = sv.MaxHpEnemy;
            Enemy.HpEnemy = sv.HpEnemy;
            Score = sv.Score;
            HighScore = sv.HighScore;
            if(isFight)
                Enemy.gameObject.transform.position = GameObject.FindWithTag("FightPosition").transform.position;
        }
        else
        {
            sv.MaxHpPlayer = Player.maxHp;
            sv.HpPlayer = Player.maxHp;
            sv.MaxHpEnemy = Enemy.maxHp;
            sv.HpEnemy = Enemy.maxHp;
            sv.Score = 0;
            HighScore = 0;
        }
    }

    public void Restart()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        // MaxHpPlayer = Player.maxHp;
        // HpPlayer = Player.maxHp;
        // MaxHpEnemy = Enemy.maxHp;
        // HpEnemy = Enemy.maxHp;
        // Player.Hp = Player.maxHp;
        // Score = 0;
        sv.MaxHpPlayer = Player.maxHp;
        sv.HpPlayer = Player.maxHp;
        sv.MaxHpEnemy = Enemy.maxHp;
        sv.HpEnemy = Enemy.maxHp;
        sv.Score = 0;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
        SceneManager.LoadScene(0);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
            Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}

[Serializable]
public class Save
{
    public int MaxHpPlayer;
    public int HpPlayer;
    public int MaxHpEnemy;
    public int HpEnemy;
    public int Score;
    public int HighScore;
}