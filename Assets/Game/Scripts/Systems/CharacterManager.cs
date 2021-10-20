using Game.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    // 0 - active; 1 - assist; 2 - off-field
    [SerializeField] private Hero[] party = new Hero[3];
    private PlayerControls _controls;
    private InputAction _movement;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _movement = _controls.Navigation.Movement;
        _movement.Enable();

        _controls.Navigation.Attack.performed += Attack;
        _controls.Navigation.Attack.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _controls.Navigation.Attack.Disable();
    }

    private void FixedUpdate()
    {
        party[0].Move(_movement.ReadValue<Vector2>());
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        party[0].Attack();
    }
}
