using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations
{
    [CreateAssetMenu(fileName = "TestAnimationAttack", menuName = "Animations/TestAnimationAttack")]
    public class TestAnimationAttack : Attack
    {
        [SerializeField]
        ActionAnimation _ActionAnimation;

        protected override IEnumerator Tick()
        {
            yield return null;
        }

        public override void Use(CharacterBase attacker)
        {
            base.Use(attacker);
            
            _ActionAnimation.AddToAnimation(target.animation);
        }
    }
}