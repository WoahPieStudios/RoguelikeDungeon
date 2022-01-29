using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        float _MoveSpeed;

        Vector2 _Velocity;

        public float moveSpeed => _MoveSpeed;

        public Vector2 velocity => _Velocity;

        void Update() 
        {
            transform.position += (Vector3)_Velocity;
        }

        public virtual void Move(Vector2 direction)
        {
            _Velocity = direction * moveSpeed * Time.fixedDeltaTime;
        }
    }
}