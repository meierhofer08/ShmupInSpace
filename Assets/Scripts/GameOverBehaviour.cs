using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverBehaviour : MonoBehaviour
{
    private Button[] _buttons;
    private Text _highscoreText;

    private void Awake()
    { 
        _buttons = GetComponentsInChildren<Button>();
        _highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        HideButtons();
    }

    private void HideButtons()
    {
        foreach (var b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
        _highscoreText.gameObject.SetActive(false);
    }

    public void ShowButtons()
    {
        foreach (var b in _buttons)
        {
            b.gameObject.SetActive(true);
        }
        _highscoreText.gameObject.SetActive(true);
        UpdateHighscoreText();
    }

    private void UpdateHighscoreText()
    {
        var score = ScoreBehaviour.Instance.GetCurrentScore();
        if (HighscoreBehaviour.Instance.NewHighscore(score))
        {
            _highscoreText.text = $"Congrats! New Highscore: {score}";
        }
        else
        {
            _highscoreText.text = $"Current Highscore: {HighscoreBehaviour.Instance.GetCurrentHighscore()}";
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");
    }
}