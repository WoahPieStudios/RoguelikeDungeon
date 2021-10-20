using System;
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

    private void Start()
    {
        party[2].gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _movement = _controls.Navigation.Movement;
        _movement.Enable();

        _controls.Navigation.Attack.performed += Attack;
        _controls.Navigation.Attack.Enable();

        _controls.Navigation.SwapAssist.performed += SwapAssist;
        _controls.Navigation.SwapAssist.Enable();
        
        _controls.Navigation.SwapOffField.performed += SwapOffField;
        _controls.Navigation.SwapOffField.Enable();

    }

    private void OnDisable()
    {
        _movement.Disable();
        _controls.Navigation.Attack.Disable();
        _controls.Navigation.SwapAssist.Disable();
        _controls.Navigation.SwapOffField.Disable();
    }

    private void Update()
    {
        party[0].isPlayerControlled = true;
    }

    private void FixedUpdate()
    {
        party[0].Move(_movement.ReadValue<Vector2>());
        party[1].Move(_movement.ReadValue<Vector2>());
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        party[0].Attack();
    }
    
    private void SwapAssist(InputAction.CallbackContext obj)
    {
        var temp = party[0];
        party[0] = party[1];
        party[1] = temp;

        party[0].transform.position = party[1].transform.position;
        party[1].isPlayerControlled = false;
        party[0].ChangeSortingOrder(1);
        party[1].ChangeSortingOrder(0);
    }
    
    private void SwapOffField(InputAction.CallbackContext obj)
    {
        party[2].gameObject.SetActive(true);
        
        var temp = party[0];
        party[0] = party[2];
        party[2] = temp;
        
        party[0].transform.position = party[2].transform.position;
        
        party[2].isPlayerControlled = false;

        party[0].ChangeSortingOrder(1);
        party[2].ChangeSortingOrder(0);
        
        party[2].gameObject.SetActive(false);
    }
}
