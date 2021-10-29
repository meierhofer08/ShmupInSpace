using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    private AudioSource _mainSource;
    
    public static SoundHelper Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundHelper!");
        }

        Instance = this;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _mainSource = GetComponentInChildren<AudioSource>();
    }

    public AudioSource GetMainSource()
    {
        return _mainSource;
    }
}
