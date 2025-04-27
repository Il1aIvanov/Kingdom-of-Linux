using UnityEngine;

namespace KingdomOfLinux.Characters
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 2f;

        private Vector2 _targetPosition;
        private bool _isMoving = false;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _targetPosition = _rigidbody2D.position;
        }

        public void MoveTo(Vector2 position)
        {
            _targetPosition = position;
            _isMoving = true;
        }

        void FixedUpdate()
        {
            if (!_isMoving)
            {
                _animator.SetFloat("Speed", 0f);
                return;
            }

            Vector2 direction = _targetPosition - _rigidbody2D.position;
            Vector2 newPos = Vector2.MoveTowards(_rigidbody2D.position, _targetPosition, moveSpeed * Time.fixedDeltaTime);

            Vector3 pos3D = new Vector3(newPos.x, newPos.y, 0f);
            _rigidbody2D.MovePosition(pos3D);

            if (Vector2.Distance(_rigidbody2D.position, _targetPosition) < 0.05f)
            {
                _isMoving = false;
            }

            _animator.SetFloat("Speed", direction.magnitude);

            // Отражение персонажа
            if (direction.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f);
        }

        void LateUpdate()
        {
            // ✅ Гарантия, что персонаж остаётся на нужной плоскости
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }
}