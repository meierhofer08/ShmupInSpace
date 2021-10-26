using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerBehaviour : MonoBehaviour
{
    [SerializeField] private float minAlpha = 0.5F;
    [SerializeField] private float maxAlpha = 1;
    [SerializeField] private float alphaStepSize = 0.01F;

    private Text _text;
    private float _currentAlpha;
    private bool _steppingDown = true;

    // Start is called before the first frame update
    private void Start()
    {
        _text = GetComponent<Text>();
        Debug.Log($"Text is {_text.gameObject.name}");
        SetAlpha(maxAlpha);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float newAlpha;
        if (_steppingDown)
        {
            newAlpha = _currentAlpha - alphaStepSize;
            if (newAlpha <= minAlpha)
            {
                newAlpha = minAlpha;
                _steppingDown = false;
            }
        }
        else
        {
            newAlpha = _currentAlpha + alphaStepSize;
            if (newAlpha >= maxAlpha)
            {
                newAlpha = maxAlpha;
                _steppingDown = true;
            }
        }

        SetAlpha(newAlpha);
    }

    private void SetAlpha(float newAlpha)
    {
        Debug.Log($"Setting new alpha {newAlpha}");
        
        _currentAlpha = newAlpha;
        var oldColor = _text.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, newAlpha);
        _text.color = newColor;
    }
}