using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;
using Game.Characters;

namespace Game.Enemies.Skeleton
{
    public class SkeletonAttack : Attack<Enemy>
    {
        [SerializeField]
        private AnimationData _AttackTopData;
        [SerializeField]
        private AnimationData _AttackBottomData;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start() 
        {
            animationHandler.AddAnimationData(_AttackTopData, End);
            animationHandler.AddAnimationData(_AttackBottomData, End);
        }

        private void Update() 
        {
            if(!isActive)
                return;
                
            if(owner.movement.velocity.magnitude > 0.001f)
            {
                animationHandler.Stop(_AttackBottomData);
            }

            else if(!animationHandler.IsAnimationPlaying(_AttackBottomData))
            {
                animationHandler.Play(_AttackBottomData);
                animationHandler.SyncAnimations(_AttackTopData, _AttackBottomData);
            }
        }

        protected override void Begin()
        {
            base.Begin();

            animationHandler.CrossFadePlay(_AttackTopData, 0.1f);
            
            if(owner.movement.velocity.magnitude <= 0.001f)
                animationHandler.CrossFadePlay(_AttackBottomData, 0.1f);
        }

        public override void End()
        {
            base.End();
            
            animationHandler.Stop(_AttackTopData);
            animationHandler.Stop(_AttackBottomData);
        }
    }
}