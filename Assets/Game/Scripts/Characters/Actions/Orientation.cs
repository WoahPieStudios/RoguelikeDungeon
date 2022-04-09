using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Orientation<T> : Action<Character>, IOrientationAction where T : Character
    {
        public abstract Vector2Int currentOrientation { get; }

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="orientation"></param>
        public virtual bool Orientate(Vector2Int orientation)
        {
            return isActive;
        }
    }
}