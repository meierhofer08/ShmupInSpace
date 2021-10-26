using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public abstract class BaseShotBehaviour : MonoBehaviour
{
    private const float MaxLifetime = 10;

    /// <summary>
    /// Damage inflicted
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile damage player or enemies?
    /// </summary>
    public bool isEnemyShot = false;

    public abstract void AdditionalShotBehaviour(GameObject otherObject);
}