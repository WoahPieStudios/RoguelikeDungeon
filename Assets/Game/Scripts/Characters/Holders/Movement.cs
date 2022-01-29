using System;
using System.Collections;
using System.Collections.Generic;
using Game.Characters.Interfaces;
using UnityEngine;

namespace Game.Characters
{
    public class Movement : MonoBehaviour, IMovementHandler, IRestrictableAction
    {
        [SerializeField]
        float _MoveSpeed;

        Vector2 _Velocity;

        public float moveSpeed => _MoveSpeed;

        public Vector2 velocity => _Velocity;

        bool _CanMove = true;

        public event Action<Vector2> onMoveEvent;

        void Update() 
        {
            transform.position += (Vector3)_Velocity;
        }

        public virtual bool Move(Vector2 direction)
        {
            if(_CanMove)
            {
                _Velocity = direction * moveSpeed * Time.fixedDeltaTime;

                onMoveEvent?.Invoke(direction);
            }

            return _CanMove;
        }

        public virtual void OnRestrict(RestrictAction restrictActions)
        {
            _CanMove = !restrictActions.HasFlag(RestrictAction.Movement); 
        }
    }
}