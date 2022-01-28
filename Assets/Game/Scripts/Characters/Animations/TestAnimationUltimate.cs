using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters.Animations
{
    public class TestAnimationUltimate : Ultimate
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        Coroutine _TickCoroutine;

        IAnimationsHandler animationHandler;

        void Awake() 
        {
            animationHandler = GetComponent<CharacterBase>().animationHandler;

            animationHandler.AddAnimation("Ultimate", _AnimationClip);
        }

        public override bool Use(Hero hero)
        {
            bool canUse = base.Use(hero);

            if(canUse)
                animationHandler.Play("Ultimate");

            return canUse;
        }
    }
}