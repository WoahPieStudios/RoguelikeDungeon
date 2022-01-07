using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations
{
    public class TestAnimationUltimate : Ultimate, IOnAssignEvent
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        Coroutine _TickCoroutine;

        protected IEnumerator Tick()
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