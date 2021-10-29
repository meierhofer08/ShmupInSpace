using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour
{
    private GameObject _powerUpPanel;
    private Image _powerUpImage;

    public static HUDBehaviour Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }
    
    private void Start()
    {
        
        _powerUpPanel = GameObject.Find("PowerUpPanel");
        _powerUpImage = _powerUpPanel.GetComponentInChildren<Image>();
        _powerUpPanel.SetActive(false);
    }

    public void SetPowerUpImage(Color color, Sprite sprite)
    {
        if (!_powerUpPanel.activeSelf)
        {
            _powerUpPanel.SetActive(true);
        }

        _powerUpImage.color = color;
        _powerUpImage.sprite = sprite;
    }
}