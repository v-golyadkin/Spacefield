using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private Text _scoreText;

    private int _scoreNumber;

    public int Score
    {
        get
        {
            return this._scoreNumber;
        }
        set
        {
            this._scoreNumber = value;
        }
    }

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
    }

    private void Start()
    {
        
    }

    public void ResetScore()
    {
        _scoreNumber = 0;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        Score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        string scoreString = string.Format("{0:0000000}", _scoreNumber);
        _scoreText.text = scoreString;
    }
}
