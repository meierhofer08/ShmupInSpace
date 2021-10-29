using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public abstract class BaseShotBehaviour : MonoBehaviour
{
    /// <summary>
    /// Damage inflicted
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile damage player or enemies?
    /// </summary>
    public bool isEnemyShot = false;

    public bool isSelfDestruct = false;

    protected bool triggered;

    public void TriggerShot()
    {
        triggered = true;
    }

    public abstract void AdditionalShotBehaviour(GameObject otherObject);
}