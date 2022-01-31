using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;
using Game.Characters.Actions;

namespace Game.Characters.Animations
{
    public class TestAnimationAttack : Attack
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        Character _NearestCharacter;

        AnimationController _AnimationController;

        IActor _Owner;

        protected override void Awake() 
        {
            base.Awake();

            _AnimationController = GetComponent<AnimationController>();

            _AnimationController.AddAnimation("Attack", _AnimationClip, End);
        }

        public override bool CanUse()
        {
            _NearestCharacter = Utilities.GetNearestCharacter<Character>(transform.position, range, owner as Character);

            return base.CanUse() && _NearestCharacter;
        }

        public override bool Use()
        {
            bool canUse = base.Use();

            if(canUse)
            {
                (owner as Character).FaceNearestCharacter(_NearestCharacter);

                _AnimationController.Play("Attack");
            }

            return canUse;
        }
    }
}