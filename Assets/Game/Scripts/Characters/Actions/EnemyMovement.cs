using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class EnemyMovement<T> : Movement<T> where T : Enemy
    {
        [SerializeField]
        float _Speed;

        public override float speed => _Speed;

        public override bool Move(Vector2 direction)
        {
            return base.Move(direction) && !isRestricted;
        }
    }
}