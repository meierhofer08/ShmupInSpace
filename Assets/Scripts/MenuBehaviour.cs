using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    private GameObject _creditsPanel;

    private void Start()
    {
        _creditsPanel = GameObject.Find("CreditsPanel");
        HideCreditsPanel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ShowCreditsPanel()
    {
        _creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        _creditsPanel.SetActive(false);
    }
}