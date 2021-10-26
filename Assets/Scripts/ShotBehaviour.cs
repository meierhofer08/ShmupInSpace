using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotBehaviour : BaseShotBehaviour
{
    private const float MaxLifetime = 10;
    
    private void Start()
    {
        Destroy(gameObject, MaxLifetime);
    }

    public override void AdditionalShotBehaviour(GameObject otherObject)
    {
        // Nothing to do here!
    }
}