using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public abstract class Orientation : MonoBehaviour
    {
        Vector2Int _CurrentDirection = Vector2Int.right;

        public Vector2Int currentDirection => _CurrentDirection;

        protected abstract void SetDirection(Vector2Int faceDirection);

        // Direction
        /// <summary>
        /// Orients the Character to the direction.
        /// </summary>
        /// <param name="faceDirection"></param>
        public void FaceDirection(Vector2Int faceDirection) // Saw it from Jolo's Code
        {
            _CurrentDirection = faceDirection;

            SetDirection(faceDirection);
        }
    }
}