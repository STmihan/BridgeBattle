using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameManager _gameManager;
    #endregion

    private void Update()
    {
        GameOverScore.text = _gameManager.Score.ToString();
        HighScore.text = _gameManager.HighScore.ToString();
        GameScore.text = _gameManager.Score.ToString();
    }

    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnDefend()
    {
        
    }

    public void OnAttack()
    {
        
    }

    public void OnPause()
    {
        isPause = true;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
        _gameManager.Restart();
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
    }

    public void OnResume()
    {
        isPause = false;
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }
}
