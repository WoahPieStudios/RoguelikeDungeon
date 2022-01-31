using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Animations
{
    public class TestAnimationUltimate : Ultimate
    {
        [SerializeField]
        AnimationClip _AnimationClip;

        Coroutine _TickCoroutine;

        AnimationController animationController;

        protected override void Awake() 
        {
            base.Awake();
            
            animationController = GetComponent<AnimationController>();

            animationController.AddAnimation("Ultimate", _AnimationClip);
        }

        public override bool Use()
        {
            bool canUse = base.Use();

            if(canUse)
                animationController.Play("Ultimate");

            return canUse;
        }
    }
}