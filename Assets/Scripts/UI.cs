using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Fields

    [Header("Game Over UI")]
    public Text GameOverScore;
    public Text HighScore;
    [Header("Game UI")]
    public Text GameScore;
    public Button Defend;
    public Button Attack;
    public Button Pause;
    [Header("Pause UI")]
    public Button Restart;
    public Button SoundPause;
    public Button Menu;
    public Button Resume;
    [Header("Start UI")]
    public Button StartButton;
    public Button SoundStart;

    [Header("UI")]
    public GameObject GameOverUI;
    public GameObject GameUI;
    public GameObject StartUI;
    public GameObject PauseUI;

    private bool isPause = false;
    public GameManager GameManager;
    #endregion
    
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        GameOverScore.text = GameManager.Score.ToString();
        HighScore.text = GameManager.HighScore.ToString();
        GameScore.text = GameManager.Score.ToString();
        if (GameManager.Player.GetComponent<Player>().Hp <= 0 || !GameManager.Player)
        {
            GameUI.SetActive(false);
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (GameOverUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameUI.SetActive(true);
                GameOverUI.SetActive(false);
                Time.timeScale = 1f;
                GameManager.Restart();
            }
        }
    }

    #region Button methods
    public void OnDefendDown()
    {
        GameManager.Player.GetComponent<Player>().BlockDown();
    }
    public void OnDefendUp()
    {
        GameManager.Player.GetComponent<Player>().BlockUp();
    }
    public void OnAttack()
    {
        GameManager.Player.GetComponent<Player>().Attack();
    }

    public void OnPause()
    {
        isPause = true;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Save();
    }

    public void OnRestart()
    {
        GameManager.Restart();
    }

    public void OnMenu()
    {
        PauseUI.SetActive(false);
        StartUI.SetActive(true);
    }

    public void OnSound()
    {
        //Sound off
    }

    public void OnStart()
    {
        StartUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Load();
    }

    public void OnResume()
    {
        isPause = false;
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }
    #endregion
}

