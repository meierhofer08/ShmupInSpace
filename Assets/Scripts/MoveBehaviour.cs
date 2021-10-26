using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;
    private bool _paused;

    private void Start()
    {
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_paused)
        {
            _movement = new Vector2(0, 0);
        }
        else
        {
            _movement = new Vector2(
                speed.x * direction.x,
                speed.y * direction.y);
        }
        
    }

    private void FixedUpdate()
    {
        _rigidbodyComponent.velocity = _movement;
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }
}