using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int MaxHpPlayer;
    public int HpPlayer;
    public int MaxHpEnemy;
    public int HpEnemy;
    public int Score;
    public int HighScore;
    
    [Space]
    public GameObject Player;
    public GameObject Spawner;
    public GameObject StartFightTrigger;

    public GameObject Enemy;

    public bool isFight = false;
    
    private Save sv = new Save();
    private string path;
    
    private void Awake()
    {
        StartFightTrigger = GameObject.FindWithTag("FightPosition");
        Spawner = GameObject.FindWithTag("Spawner");
        Enemy = GameObject.FindWithTag("Enemy");
        Time.timeScale = 0;
        Load();
    }

    private void Update()
    {
        if (HighScore < Score) HighScore=Score;
        Enemy = GameObject.FindWithTag("Enemy");
        Player = GameObject.FindWithTag("Player");
    }

    public void ScoreUp()
    {
        Score++;
    }

    public void Save()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        sv.MaxHpPlayer = player.maxHp;
        sv.HpPlayer = player.Hp;
        sv.MaxHpEnemy = enemy.maxHp;
        sv.HpEnemy = enemy.Hp;
        sv.Score = Score;
        sv.HighScore = HighScore;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }

    public void Load()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            player.maxHp = sv.MaxHpPlayer;
            player.Hp = sv.HpPlayer;
            enemy.maxHp = sv.MaxHpEnemy;
            enemy.Hp = sv.HpEnemy;
            Score = sv.Score;
            HighScore = sv.HighScore;
            if(isFight)
                Enemy.gameObject.transform.position = GameObject.FindWithTag("FightPosition").transform.position;
        }
        else
        {
            sv.MaxHpPlayer = player.maxHp;
            sv.HpPlayer = player.maxHp;
            sv.MaxHpEnemy = enemy.maxHp;
            sv.HpEnemy = enemy.maxHp;
            sv.Score = 0;
            HighScore = 0;
        }
    }

    public void Restart()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        MaxHpPlayer = player.maxHp;
        HpPlayer = player.maxHp;
        MaxHpEnemy = enemy.maxHp;
        HpEnemy = enemy.maxHp;
        player.Hp = player.maxHp;
        Score = 0;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
        var plaerTr = player.gameObject.transform.position;
        Destroy(enemy.gameObject);
        Destroy(player.gameObject);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Spawner.GetComponent<Spawner>().Spawn(0);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Spawner.GetComponent<Spawner>().PlayerRespawn();
        isFight = false;
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