using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance; 

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage; 
    public Text scoreText;

    enum PageState 
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    private int score = 0;
    private bool gameOver = false;

    public bool GameOver { get { return gameOver; } }

    private void Awake() 
    {
        Instance = this;
    }

    private void OnEnable() 
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        Unicorn.OnPlayerDied += OnPlayerDied;
        Unicorn.OnPlayerScored += OnPlayerScored;
    }

    private void OnDisable() 
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        Unicorn.OnPlayerDied -= OnPlayerDied;
        Unicorn.OnPlayerScored -= OnPlayerScored;
    }

    private void OnCountdownFinished() 
    {
        SetPageState(PageState.None);
        OnGameStarted(); //event set for Unicorn
        score = 0;
        gameOver = false;
    }

    private void OnPlayerDied() 
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("Highscore");

        if (score > savedScore) 
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        SetPageState(PageState.GameOver);
    }

    private void OnPlayerScored() 
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void SetPageState(PageState state) 
    {
        switch (state) 
        {
            case  PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);

                break;
            case  PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);

                break;
            case  PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);

                break;
            case  PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);

                break;
        }
    }

    public void ConfirmGameOver() 
    {
         //activated when replay button is hit
         OnGameOverConfirmed(); //event set for Unicorn
        scoreText.text = "0";
        SetPageState(PageState.Start);
    }

    public void StartGame() 
    {
        //activated when play button is hit
        SetPageState(PageState.Countdown);
    }
}
