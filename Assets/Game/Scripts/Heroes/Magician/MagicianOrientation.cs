using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Heroes.Magician
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MagicianOrientation : Orientation
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
                transform.localScale = new Vector3(orientation.x == 0 ? transform.localScale.x : Mathf.Sign(orientation.x), 1, 1);

                _CurrentOrientation = orientation;
            }

            return canOrientate;
        }
    }
}