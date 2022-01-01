using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations
{
    [CreateAssetMenu(fileName = "TestAnimationAttack", menuName = "Animations/TestAnimationAttack")]
    public class TestAnimationAttack : Attack, IOnAssignEvent
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        CharacterBase _NearestCharacter;

        protected override IEnumerator Tick()
        {
            target.FaceNearestCharacter(_NearestCharacter);

            yield return new WaitForSeconds(2);
        }

        public override bool CanUse(CharacterBase attacker)
        {
            _NearestCharacter = Utilities.GetNearestCharacter<CharacterBase>(attacker.transform.position, range, attacker);

            return base.CanUse(attacker) && _NearestCharacter;
        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);

            target.animationController.Play("Attack");
        }

        public override void End()
        {
            base.End();
        }

        public void OnAssign(CharacterBase character)
        {
            character.animationController.AddAnimation("Attack", _AnimationClip, End);
        }
    }
}