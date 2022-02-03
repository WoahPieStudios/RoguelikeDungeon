using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class Orientation : ActorAction, IOrientationAction
    {
        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public abstract Vector2Int currentOrientation { get; }

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="orientation"></param>
        public virtual bool Orientate(Vector2Int orientation)
        {
            return isActive && !isRestricted;
        }

        public virtual void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Orientation);
        }

        public virtual void ToggleAction(bool isActive)
        {
            if(isActive)
                Begin();

            else
                End();
        }

        public virtual bool Use()
        {
            Begin();

            return isActive;
        }
    }
}