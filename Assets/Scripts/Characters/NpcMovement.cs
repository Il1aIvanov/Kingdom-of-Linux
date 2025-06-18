using KingdomOfLinux.Controllers;
using UnityEngine;

namespace KingdomOfLinux.Characters
{
    public class NpcMovement : MonoBehaviour
    {
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float minDirectionTime = 5f;
        [SerializeField] private float maxDirectionTime = 10f;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _movement;
        private float _directionTimer;
        private int _direction;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ChooseNewDirection();
        }

        private void Update()
        {
            if (PauseController.IsGamePaused)
            {
                _movement = Vector2.zero;
                _animator.SetFloat(XVelocity, 0);
                return;
            }
            _directionTimer -= Time.deltaTime;
            if (_directionTimer <= 0f)
            {
                ChooseNewDirection();
            }
            _movement = new Vector2(_direction, 0f);
            _animator.SetFloat(XVelocity, Mathf.Abs(_movement.x));
            if (_direction != 0)
                _spriteRenderer.flipX = _direction < 0;
        }

        private void FixedUpdate()
        {
            if (PauseController.IsGamePaused) return;
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void ChooseNewDirection()
        {
            _direction = Random.Range(-1, 2);
            _directionTimer = Random.Range(minDirectionTime, maxDirectionTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (PauseController.IsGamePaused) return;
            if (!other.CompareTag($"Border")) return;
            _direction *= -1;
            _directionTimer = Random.Range(minDirectionTime, maxDirectionTime);

            _spriteRenderer.flipX = _direction < 0;
        }
    }
}