using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class EnemyMovement<T> : EnemyAction<T>, IMovementAction where T : Enemy
    {
        [SerializeField]
        Property _Speed;
        
        bool _IsRestricted = false;

        public Property speed => _Speed;

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(speed);
        }

        public virtual bool Move(Vector2 direction)
        {
            return isActive && !isRestricted;
        }

        public virtual void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Movement);
        }
    }
}