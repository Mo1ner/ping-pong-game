using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _ball;

    private int _playerScore = 0;
    private int _computerScore = 0;
    private int _winningScore = 3;
    
    private Hud _hud;
    private GameObject _hudCanvas;
    private GameObject _paddleComputer;
    public GameState _gameState = GameState.Launched; 
    
    public enum GameState
    {
        Playing,
        GameOver,
        Paused,
        Launched
    }
    void Start()
    {
        _ball = GameObject.Find("Ball");
        _hudCanvas = GameObject.Find("HUD_Canvas");
        _hud = _hudCanvas.GetComponent<Hud>();
        _paddleComputer = GameObject.Find("Computer");
        
        _hud._playAgain.text = "PRESS SPACE TO PLAY";
    }

     void Update()
    {
        CheckScore();
        CheckInput();
    }

    void CheckScore()
    {
        if (_playerScore >= _winningScore || _computerScore >= _winningScore)
        {
            if (_playerScore >= _winningScore && _computerScore < _playerScore - 1)
            {
                PLayerWin();
            }

            if (_computerScore >= _winningScore && _playerScore < _computerScore - 1)
            {
                ComputerWin();
            }
        }
    }
  
    private void PLayerWin()
    {
        _hud._playerWinText.enabled = true;
        GameOver();
    }
  
    private void ComputerWin()
    {
        _hud._computerWinText.enabled = true;
        GameOver();
    }
    void SpawnBall()
    {
        _ball = Instantiate((GameObject)Resources.Load("Prefabs/Ball", typeof(GameObject)));
        transform.position = new Vector3(0f, 0f, 0f);
    }

    public void PlayerPoint()
    {
        _playerScore++;
        _hud._playerScoreText.text = _playerScore.ToString();
        NextRound();
    }

    public void ComputerPoint()
    {
        _computerScore++;
        _hud._computerScoreText.text = _computerScore.ToString();
        NextRound();
    }

    void CheckInput()
    {
        if (_gameState == GameState.Paused || _gameState == GameState.Playing)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PauseResumeGame();
            }
        }
        if (_gameState == GameState.Launched || _gameState == GameState.GameOver)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartGame();
            }
        }
    }    
    
    private void StartGame()
    {
        _playerScore = 0;
        _computerScore = 0;

        _hud._playerScoreText.text = "0";
        _hud._computerScoreText.text = "0";
        _hud._playerWinText.enabled = false;
        _hud._computerWinText.enabled = false;
        _hud._playAgain.enabled = false;

        _gameState = GameState.Playing;
        
        _paddleComputer.transform.localPosition = new Vector3(_paddleComputer.transform.localPosition.x, 0f, _paddleComputer.transform.localPosition.z);
        SpawnBall();
    }
    
    private void GameOver()
    {
        Destroy(_ball.gameObject);
        _hud._playAgain.text = "PRESS SPACE TO PLAY AGAIN";
        _hud._playAgain.enabled = true;
        _gameState = GameState.GameOver;
    }

    private void PauseResumeGame()
    {
        if (_gameState == GameState.Paused)
        {
            _gameState = GameState.Playing;
            _hud._playAgain.enabled = false;
        }
        else
        {
            _gameState = GameState.Paused;
            _hud._playAgain.text = "GAME IS PAUSED, PRESS SPACE TO PLAY";
            _hud._playAgain.enabled = true;
        }
    }
    
    public void NextRound()
    {
        if (_gameState == GameState.Playing)
        {
            _paddleComputer.transform.localPosition = new Vector3(_paddleComputer.transform.localPosition.x, 0, _paddleComputer.transform.localPosition.z);
            Destroy(_ball.gameObject);
            SpawnBall();
        }
    }
}
