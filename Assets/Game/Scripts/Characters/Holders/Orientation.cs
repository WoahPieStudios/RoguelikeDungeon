using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class Orientation : MonoBehaviour, IOrientationHandler, IRestrictableAction
    {
        Vector2Int _CurrentDirection = Vector2Int.right;

        public Vector2Int currentDirection => _CurrentDirection;

        bool _CanOrient = true;

        protected abstract Vector2Int SetDirection(Vector2Int faceDirection);

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="faceDirection"></param>
        public virtual bool FaceDirection(Vector2Int faceDirection)
        {
            if(_CanOrient)
                _CurrentDirection = SetDirection(faceDirection);
            
            return _CanOrient;
        }

        public virtual void OnRestrict(RestrictAction restrictActions)
        {
            _CanOrient = !restrictActions.HasFlag(RestrictAction.Orientation);
        }
    }
}