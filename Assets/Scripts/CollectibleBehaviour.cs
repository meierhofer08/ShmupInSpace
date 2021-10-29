using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public GameObject CollectibleWeapon;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (transform.position.x < Camera.main.transform.position.x &&
            !_renderer.IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }
}
