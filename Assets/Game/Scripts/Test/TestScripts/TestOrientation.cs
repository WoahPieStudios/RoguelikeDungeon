using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TestOrientation : Orientation<Character>
    {
        Vector2Int _CurrentOrientation;
        SpriteRenderer _SpriteRenderer;

        public override Vector2Int currentOrientation => _CurrentOrientation;

        protected override void Awake()
        {
            base.Awake();

            Use();
            
            _SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override bool Orientate(Vector2Int orientation)
        {
            bool canOrientate = base.Orientate(orientation);

            if(canOrientate)
            {
                _CurrentOrientation = orientation;

                _SpriteRenderer.flipX = orientation.x == 0 ? _SpriteRenderer.flipX : !(Mathf.Sign(orientation.x) == 1);
            }

            return canOrientate;
        }
    }
}