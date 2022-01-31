using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Game.Actions;

namespace Game.Characters.Actions
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TestOrientation : Orientation
    {
        Vector2Int _CurrentOrientation;
        SpriteRenderer _SpriteRenderer;

        public override Vector2Int currentOrientation => _CurrentOrientation;

        protected override void Awake()
        {
            base.Awake();

            ToggleAction(true);
            
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