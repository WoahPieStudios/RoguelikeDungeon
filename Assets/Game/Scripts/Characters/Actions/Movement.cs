using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class Movement : Action, IMovementAction
    {
        bool _IsRestricted = false;

        public abstract Property speed { get; }

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(speed);
        }

        public virtual bool Move(Vector2 direction)
        {
            return isActive;
        }

        public virtual void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Movement);
        }
    }
}