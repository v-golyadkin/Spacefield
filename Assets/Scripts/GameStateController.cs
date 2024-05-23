using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject enemySpawner;

    [SerializeField] private GameObject scoreBar;
    [SerializeField] private GameObject gameOver;
    public enum GameState
    {
        GameStart,
        Gameplay,
        GameOver,
    }

    private GameState gameState;
    
    private void Start()
    {
        gameState = GameState.GameStart;
    }

    private void UpdateGameState()
    {
        switch(gameState)
        {
            case GameState.GameStart:

                startButton.SetActive(true);

                gameOver.SetActive(false);

                break;

            case GameState.Gameplay:

                startButton.SetActive(false);

                scoreBar.GetComponentInChildren<GameScore>().ResetScore();

                enemySpawner.GetComponent<EnemySpawner>().StartObjectSpawn();

                playerShip.GetComponent<Character>().Init();

                break;

            case GameState.GameOver:

                enemySpawner.GetComponent<EnemySpawner>().StopObjectSpawn();

                gameOver.SetActive(true);

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
        gameState = state;
        UpdateGameState();
    }

    public void StartGame()
    {
        gameState = GameState.Gameplay;
        UpdateGameState();
    }
}
