using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteOrientation : Orientation
    {
        SpriteRenderer _SpriteRenderer;

        protected virtual void Awake() 
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void SetDirection(Vector2Int faceDirection)
        {
            _SpriteRenderer.flipX = !(Mathf.Sign(faceDirection.x) == 1);
        }
    }
}