using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class Movement : ActorAction, IMovementAction
    {
        bool _IsRestricted = false;

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

        public virtual bool Move(Vector2 direction)
        {
            return isActive && !isRestricted;
        }

        public virtual void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Movement); 

            Debug.Log(_IsRestricted);
        }

        public virtual void ToggleAction(bool isActive)
        {
            if(isActive)
                Begin();

            else
                End();
        }
    }
}