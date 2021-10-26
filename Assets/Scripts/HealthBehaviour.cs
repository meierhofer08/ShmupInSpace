using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private bool isEnemy = true;

    [SerializeField] private AudioClip deathClip;
    [SerializeField] private float deathVolume = 0.6F;

    public void Kill()
    {
        Damage(hp);
    }

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp > 0)
        {
            return;
        }

        SpecialEffectsHelper.Instance.Explosion(transform.position);
        if (deathClip != null)
        {
            SoundHelper.Instance.GetMainSource().PlayOneShot(deathClip, deathVolume);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var shot = otherCollider.gameObject.GetComponent<BaseShotBehaviour>();
        if (shot == null || shot.isEnemyShot == isEnemy)
        {
            return;
        }

        shot.AdditionalShotBehaviour(gameObject);
        Damage(shot.damage);
        Destroy(shot.gameObject);
    }
}