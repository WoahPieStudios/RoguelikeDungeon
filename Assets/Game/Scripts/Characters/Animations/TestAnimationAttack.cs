using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations
{
    public class TestAnimationAttack : Attack, IOnAssignEvent
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        CharacterBase _NearestCharacter;

        public override bool CanUse(CharacterBase attacker)
        {
            _NearestCharacter = Utilities.GetNearestCharacter<CharacterBase>(attacker.transform.position, range, attacker);

            return base.CanUse(attacker) && _NearestCharacter;
        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);
            
            target.FaceNearestCharacter(_NearestCharacter);

            target.animationController.Play("Attack");
        }

        public void OnAssign(CharacterBase character)
        {
            character.animationController.AddAnimation("Attack", _AnimationClip, End);
        }
    }
}