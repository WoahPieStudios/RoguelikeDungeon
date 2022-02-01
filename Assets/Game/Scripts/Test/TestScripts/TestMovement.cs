using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Test
{
    public class TestMovement : Movement
    {
        [SerializeField]
        float _MoveSpeed;
        [SerializeField]
        float _LerpTime;

        Vector2 _Velocity;
        Vector2 _Direction;

        public override Vector2 velocity => _Velocity;

        protected override void Awake()
        {
            base.Awake();

            ToggleAction(true);
        }

        void FixedUpdate() 
        {
            if(isActive && !isRestricted)
            {
                _Velocity = Vector2.Lerp(velocity, _Direction * _MoveSpeed * Time.fixedDeltaTime, _LerpTime);

                transform.position += (Vector3)_Velocity;
            }
        }

        public override bool Move(Vector2 direction)
        {
            bool canMove = base.Move(direction);

            if(canMove)
                _Direction = direction;

            return canMove;
        }
    }
}