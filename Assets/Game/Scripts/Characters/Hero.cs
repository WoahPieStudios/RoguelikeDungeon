using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D _rigidbody;

    [Header("Animations")]
    private Animator _animator;
    private bool _isFlipped;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _animator.SetBool("isRun", direction != Vector2.zero);
        OrientPlayer(direction);

        _rigidbody.MovePosition(_rigidbody.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void OrientPlayer(Vector2 direction)
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

    public void Attack()
    {
        _animator.SetTrigger("attack");
    }
    
}
