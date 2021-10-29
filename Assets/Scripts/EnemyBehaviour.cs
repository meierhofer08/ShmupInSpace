using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float minDelayFirstShot = 0;
    [SerializeField] private float maxDelayFirstShot = 1;
    [SerializeField] private int scoreForFreeze = 30;

    private bool _hasSpawn;
    private MoveBehaviour _moveBehaviour;
    private WeaponBehaviour _weapon;
    private Collider2D _colliderComponent;
    private SpriteRenderer _rendererComponent;
    private Color _initialColor;
    private float _frozenTimeLeft;
    private double _delayFirstShot;

    private void Awake()
    {
        _weapon = GetComponentInChildren<WeaponBehaviour>();
        _moveBehaviour = GetComponent<MoveBehaviour>();
        _colliderComponent = GetComponent<Collider2D>();
        _rendererComponent = GetComponent<SpriteRenderer>();
        _initialColor = _rendererComponent.color;
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
                if (_frozenTimeLeft <= 0) // Unfreeze
                {
                    _rendererComponent.color = _initialColor;
                    _moveBehaviour.Unpause();
                    _weapon.enabled = true;
                }
            }

            if (_weapon != null && _weapon.enabled && _weapon.CanAttack)
            {
                if (_delayFirstShot > 0)
                {
                    _delayFirstShot -= Time.deltaTime;
                }
                else
                {
                    _weapon.Attack(true, transform.position);
                }
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
        _delayFirstShot = Random.Range(minDelayFirstShot, maxDelayFirstShot);
    }

    public void Freeze(float seconds, AudioClip freezeClip, Color32 frozenColor)
    {
        _frozenTimeLeft = seconds;
        _moveBehaviour.Pause();
        _weapon.enabled = false;
        _rendererComponent.color = frozenColor;
        ScoreBehaviour.Instance.IncreaseScore(scoreForFreeze);
        if (freezeClip != null)
        {
            SoundHelper.Instance.GetMainSource().PlayOneShot(freezeClip);
        }
    }
}