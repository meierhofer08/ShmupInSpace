using System;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform shotPrefab;
    [SerializeField] private float shootingRate = 0.25f;
    public bool boundShots = false; // whether shots are bound to shooter's position

    [SerializeField] private AudioClip shotClip;
    [SerializeField] private float shotVolume = 0.6F;

    private float _shootCooldown;
    private Action<Rigidbody2D> _setShotBodyAction;

    private void Start()
    {
        _shootCooldown = 0f;
    }

    private void Update()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
    }

    public void SetShotBodyAction(Action<Rigidbody2D> setShotBodyAction)
    {
        _setShotBodyAction = setShotBodyAction;
    }

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy, Vector3 position)
    {
        if (!CanAttack)
        {
            return;
        }

        _shootCooldown = shootingRate;

        var shotTransform = Instantiate(shotPrefab);
        shotTransform.position = position;

        // Set the shot rigidbody onto the weapon's owner so it can move with the owner
        if (boundShots)
        {
            var shotRigidbody = shotTransform.gameObject.GetComponent<Rigidbody2D>();
            _setShotBodyAction.Invoke(shotRigidbody);
        }

        var shot = shotTransform.gameObject.GetComponent<BaseShotBehaviour>();
        if (shot != null)
        {
            shot.TriggerShot();
            shot.isEnemyShot = isEnemy;
        }

        if (shotClip != null)
        {
            SoundHelper.Instance.GetMainSource().PlayOneShot(shotClip, shotVolume);
        }

        // Make the weapon shot always towards it
        var move = shotTransform.gameObject.GetComponent<MoveBehaviour>();
        if (move != null)
        {
            move.direction = transform.right;
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack => _shootCooldown <= 0f;
}