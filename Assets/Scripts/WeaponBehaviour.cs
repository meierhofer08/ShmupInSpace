using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform shotPrefab;
    [SerializeField] private float shootingRate = 0.25f;

    [SerializeField] private AudioClip shotClip;
    [SerializeField] private float shotVolume = 0.6F;

    private float _shootCooldown;

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

        var shot = shotTransform.gameObject.GetComponent<ShotBehaviour>();
        if (shot != null)
        {
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