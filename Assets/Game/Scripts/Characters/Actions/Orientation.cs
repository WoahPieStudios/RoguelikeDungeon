using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Orientation : Action<Character>
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

        public bool Use()
        {
            if(!isActive)
                Begin();

            return isActive;
        }
    }
}