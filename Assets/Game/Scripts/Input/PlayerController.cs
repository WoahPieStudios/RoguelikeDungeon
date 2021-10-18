using UnityEngine;

namespace Game.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private PlayerControls _controls;
        private Animator _animator;
        private bool _isFlipped;

        private void Awake()
        {
            _controls = new PlayerControls();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Start()
        {
            _controls.Navigation.Attack.performed += _ => Attack();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var direction = _controls.Navigation.Movement.ReadValue<Vector2>();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            _animator.SetBool("isRun", direction != Vector2.zero);
            OrientPlayer(direction.x);
        }

        private void OrientPlayer(float direction)
        {
            if (direction < 0)
            {
                _isFlipped = true;
            }
            else if (direction > 0)
            {
                _isFlipped = false;
            }
            
            transform.rotation = _isFlipped ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        private void Attack()
        {
            _animator.SetTrigger("attack");
        }
    }
}
