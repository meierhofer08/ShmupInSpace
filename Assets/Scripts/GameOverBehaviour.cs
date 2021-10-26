using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverBehaviour : MonoBehaviour
{
    private Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        HideButtons();
    }

    private void HideButtons()
    {
        foreach (var b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in _buttons)
        {
            b.gameObject.SetActive(true);
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