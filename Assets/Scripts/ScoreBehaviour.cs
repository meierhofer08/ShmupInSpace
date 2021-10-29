using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehaviour : MonoBehaviour
{
    public static ScoreBehaviour Instance;

    private int _currentScore = 0;
    private readonly object _currentScoreLock = new object();
    private Text _scoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of HighScoreBehaviour!");
        }

        Instance = this;
    }
    
    private void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        _scoreText.text = $"Score: {_currentScore}";
    }

    public void IncreaseScore(int amount)
    {
        lock (_currentScoreLock)
        {
            _currentScore += amount;
        }
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }
}
