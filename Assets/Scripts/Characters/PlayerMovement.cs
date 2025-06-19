using KingdomOfLinux.Controllers;
using UnityEngine;

namespace KingdomOfLinux.Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _movement;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (PauseController.IsGamePaused)
            {
                _movement = Vector2.zero;
                _animator.SetFloat(XVelocity, 0);
                return;
            }
            var horizontal = 0.0f;
            if (Input.GetKey(KeyCode.D))
                horizontal = 1;
            if (Input.GetKey(KeyCode.A))
                horizontal = -1;
            _movement = new Vector2(horizontal, 0);
            _animator.SetFloat(XVelocity, Mathf.Abs(horizontal));
            if (horizontal != 0)
                _spriteRenderer.flipX = horizontal < 0;
        }

        private void FixedUpdate()
        {
            if (PauseController.IsGamePaused) return;
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
