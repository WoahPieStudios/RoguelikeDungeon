using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters.Animations
{
    public class TestAnimationAttack : Attack
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        CharacterBase _NearestCharacter;

        IAnimationsHandler animationHandler;

        void Awake() 
        {
            animationHandler = GetComponent<CharacterBase>().animationHandler;

            animationHandler.AddAnimation("Attack", _AnimationClip, End);
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _NearestCharacter = Utilities.GetNearestCharacter<CharacterBase>(attacker.transform.position, range, attacker);

            return base.CanUse(attacker) && _NearestCharacter;
        }

        public override bool Use(CharacterBase attacker)
        {
            bool canUse = base.Use(attacker);

            if(canUse)
            {
                target.FaceNearestCharacter(_NearestCharacter);

                animationHandler.Play("Attack");
            }

            return canUse;
        }
    }
}