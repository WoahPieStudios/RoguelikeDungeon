using UnityEngine;

namespace Game.Characters.Temp
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Character Class", order = 0)]
        
        [Header("Movement", order = 1)]
        [SerializeField] protected float movementSpeed;
        protected Rigidbody2D _rigidbody;

        [Header("Animations")]
        protected Animator _animator;
        private bool _isFlipped;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual void Move(Vector2 direction)
        {
            _animator.SetBool("isRun", direction != Vector2.zero);
            OrientPlayer(direction);

            _rigidbody.MovePosition(_rigidbody.position + direction * movementSpeed * Time.fixedDeltaTime);
        }

        protected virtual void OrientPlayer(Vector2 direction)
        {
            if (direction.x < 0)
            {
                _isFlipped = true;
            }
            else if (direction.x > 0)
            {
                _isFlipped = false;
            }
            
            transform.rotation = _isFlipped ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        public virtual void Attack()
        {
            _animator.SetTrigger("attack");
        }
    }
}