using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SpriteCharacter : CharacterBase
    {
        SpriteRenderer _SpriteRenderer;

        SpriteRenderer spriteRenderer => _SpriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            _SpriteRenderer = GetComponent<SpriteRenderer>();

            boxCollider2D.size = _SpriteRenderer.size;
        }

        public override void Orient(Vector2Int faceDirection)
        {
            base.Orient(faceDirection);

            _SpriteRenderer.flipX = !(Mathf.Sign(faceDirection.x) == 1);
        }
    }
}