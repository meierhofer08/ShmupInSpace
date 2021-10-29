using System;
using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(50, 50);
    [SerializeField] private AudioClip powerUpClip;

    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;

    private WeaponBehaviour _standardWeapon;
    private GameObject _collectedWeapon;
    private Rigidbody2D _shotRigidbody;

    private void Start()
    {
        // 5 - Get the component and store the reference
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
        _standardWeapon = GetComponentInChildren<WeaponBehaviour>();
    }

    private void Update()
    {
        // 3 - Retrieve axis information
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        _movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        // 5 - Shooting
        TryShoot("Fire1", _standardWeapon);
        if (_collectedWeapon != null)
        {
            TryShoot("Fire2", _collectedWeapon.GetComponent<WeaponBehaviour>());
        }

        // Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
        );
    }

    private void TryShoot(string button, WeaponBehaviour weapon)
    {
        var shoot = Input.GetButton(button);
        if (shoot && weapon != null)
        {
            weapon.Attack(false, transform.position);
        }
    }

    private void FixedUpdate()
    {
        _rigidbodyComponent.velocity = _movement;
        if (_shotRigidbody != null)
        {
            var shotMovement = new Vector2(_movement.x + 1 /* dirty hack to account for player's scrolling */,
                _movement.y);
            _shotRigidbody.velocity = shotMovement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // Collision with enemy
        var enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            // Kill the enemy
            var enemyHealth = enemy.GetComponent<HealthBehaviour>();
            if (enemyHealth != null) enemyHealth.Kill();

            damagePlayer = true;
        }

        // Damage the player
        if (damagePlayer)
        {
            var playerHealth = GetComponent<HealthBehaviour>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectible = other.gameObject.GetComponent<CollectibleBehaviour>();
        if (collectible == null)
        {
            return;
        }

        if (_collectedWeapon != null)
        {
            Destroy(_collectedWeapon);
        }
        
        _collectedWeapon = Instantiate(collectible.CollectibleWeapon);
        var weaponBehaviour = _collectedWeapon.GetComponent<WeaponBehaviour>();
        if (weaponBehaviour.boundShots)
        {
            weaponBehaviour.SetShotBodyAction(r2d => _shotRigidbody = r2d);
        }
        
        Destroy(other.gameObject);
        if (powerUpClip != null)
        {
            SoundHelper.Instance.GetMainSource().PlayOneShot(powerUpClip);
        }

        var collectibleRenderer = other.gameObject.GetComponent<SpriteRenderer>();
        HUDBehaviour.Instance.SetPowerUpImage(collectibleRenderer.color, collectibleRenderer.sprite);
    }

    private void OnDestroy()
    {
        // Game Over.
        var gameOver = FindObjectOfType<GameOverBehaviour>();
        if (gameOver != null)
        {
            gameOver.ShowButtons();
        }
    }
}