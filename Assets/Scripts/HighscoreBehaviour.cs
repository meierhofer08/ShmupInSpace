using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreBehaviour : MonoBehaviour
{
    private int _currentHighscore;

    public static HighscoreBehaviour Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool NewHighscore(int score)
    {
        if (score > _currentHighscore)
        {
            _currentHighscore = score;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetCurrentHighscore()
    {
        return _currentHighscore;
    }
}
