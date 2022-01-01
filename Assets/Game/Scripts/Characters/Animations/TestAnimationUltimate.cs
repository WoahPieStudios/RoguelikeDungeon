using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations
{
    [CreateAssetMenu(fileName = "TestAnimationUltimate", menuName = "Animations/TestAnimationUltimate")]
    public class TestAnimationUltimate : Ultimate, IOnAssignEvent
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        protected override void OnCooldown()
        {
            
        }

        protected override IEnumerator Tick()
        {
            yield return new WaitForSeconds(2);

            End();
        }

        public override void Activate(Hero hero)
        {
            base.Activate(hero);

            hero.animationController.Play("Ultimate");
        }

        public void OnAssign(CharacterBase character)
        {
            character.animationController.AddAnimation("Ultimate", _AnimationClip);
        }
    }
}