using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Movement<T> : Action<T>, IMovementAction where T : Character
    {
        bool _IsRestricted = false;

        public abstract float speed { get; }

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

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