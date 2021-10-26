using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    private bool _hasSpawn;
    private MoveBehaviour _moveBehaviour;
    private WeaponBehaviour _weapon;
    private Collider2D _colliderComponent;
    private SpriteRenderer _rendererComponent;
    private float _frozenTimeLeft;

    private void Awake()
    {
        _weapon = GetComponentInChildren<WeaponBehaviour>();

        _moveBehaviour = GetComponent<MoveBehaviour>();

        _colliderComponent = GetComponent<Collider2D>();

        _rendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _hasSpawn = false;

        // Disable everything
        _colliderComponent.enabled = false;
        _moveBehaviour.enabled = false;
        _weapon.enabled = false;
    }

    private void Update()
    {
        if (_hasSpawn == false)
        {
            if (_rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            if (_frozenTimeLeft > 0)
            {
                _frozenTimeLeft -= Time.deltaTime;
                if (_frozenTimeLeft <= 0)
                {
                    _moveBehaviour.Unpause();
                    _weapon.enabled = true;
                }
            }

            if (_weapon != null && _weapon.enabled && _weapon.CanAttack)
            {
                _weapon.Attack(true, transform.position);
            }

            if (_rendererComponent.transform.position.x < Camera.main.transform.position.x &&
                _rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Spawn()
    {
        _hasSpawn = true;

        // Enable everything
        _colliderComponent.enabled = true;
        _moveBehaviour.enabled = true;
        _weapon.enabled = true;
    }

    public void Freeze(float seconds, AudioClip freezeClip)
    {
        // TODO: display frozen state
        _frozenTimeLeft = seconds;
        _moveBehaviour.Pause();
        _weapon.enabled = false;
        if (freezeClip != null)
        {
            SoundHelper.Instance.GetMainSource().PlayOneShot(freezeClip);
        }
    }
}