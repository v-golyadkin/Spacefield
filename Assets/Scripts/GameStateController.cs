using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _playerShip;
    [SerializeField] private GameObject _enemySpawner;

    [SerializeField] private GameObject _scoreBar;
    [SerializeField] private GameObject _gameOver;
    public enum GameState
    {
        GameStart,
        Gameplay,
        GameOver,
    }

    private GameState _gameState;
    
    private void Start()
    {
        _gameState = GameState.GameStart;
    }

    private void UpdateGameState()
    {
        switch(_gameState)
        {
            case GameState.GameStart:

                _startButton.SetActive(true);

                _gameOver.SetActive(false);

                break;

            case GameState.Gameplay:

                _startButton.SetActive(false);

                _scoreBar.GetComponentInChildren<GameScore>().ResetScore();

                _enemySpawner.GetComponent<ObjectSpawner>().StartObjectSpawn();

                _playerShip.GetComponent<Character>().Init();

                break;

            case GameState.GameOver:

                _enemySpawner.GetComponent<ObjectSpawner>().StopObjectSpawn();

                _gameOver.SetActive(true);

                Invoke("ChangeToStartState", 10f);

                break;
        }
    }

    private void ChangeToStartState()
    {
        SetGameState(GameState.GameStart);
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;
        UpdateGameState();
    }

    public void StartGame()
    {
        _gameState = GameState.Gameplay;
        UpdateGameState();
    }
}
