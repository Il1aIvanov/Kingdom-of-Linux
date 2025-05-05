using UnityEngine;

namespace KingdomOfLinux.Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _movement;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            var horizontal = 0.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                horizontal = 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                horizontal = -1;

            _movement = new Vector2(horizontal, 0);

            // Анимация
            
            _animator.SetFloat("xVelocity", Mathf.Abs(horizontal));

            // Флип спрайта
            if (horizontal != 0)
                _spriteRenderer.flipX = horizontal < 0;
        }

        void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
