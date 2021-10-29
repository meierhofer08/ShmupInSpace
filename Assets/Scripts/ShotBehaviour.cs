using System;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotBehaviour : BaseShotBehaviour
{
    private const float MaxLifetime = 10;

    private SpriteRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, MaxLifetime);
    }

    private void Update()
    {
        if (transform.position.x < Camera.main.transform.position.x &&
            !_renderer.IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }

    public override void AdditionalShotBehaviour(GameObject otherObject)
    {
        // Nothing to do here!
    }
}