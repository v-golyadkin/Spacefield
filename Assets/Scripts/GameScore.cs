using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private Text scoreText;

    private int scoreNumber;

    public int Score
    {
        get
        {
            return this.scoreNumber;
        }
        set
        {
            this.scoreNumber = value;
        }
    }

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void Start()
    {
        
    }

    public void ResetScore()
    {
        scoreNumber = 0;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        Score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        string scoreString = string.Format("{0:0000000}", scoreNumber);
        scoreText.text = scoreString;
    }
}
