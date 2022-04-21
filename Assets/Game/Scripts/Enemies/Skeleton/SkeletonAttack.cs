using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;
using Game.Characters;
using Game.Properties;

namespace Game.Enemies.Skeleton
{
    public class SkeletonAttack : EnemyAttack<Skeleton>
    {
        [SerializeField]
        private AnimationData _AttackTopData;
        [SerializeField]
        private AnimationData _AttackBottomData;

        private void Start() 
        {
            owner.animationHandler.AddAnimationData(_AttackTopData, End);
            owner.animationHandler.AddAnimationData(_AttackBottomData, End);
        }

        private void Update() 
        {
            if(!isActive || isRestricted)
                return;
                
            if(owner.movement.velocity.magnitude > 0.001f)
            {
                owner.animationHandler.Stop(_AttackBottomData);
            }

            else if(!owner.animationHandler.IsAnimationPlaying(_AttackBottomData))
            {
                owner.animationHandler.Play(_AttackBottomData);
                owner.animationHandler.SyncAnimations(_AttackTopData, _AttackBottomData);
            }
        }

        protected override void OnUse()
        {
            base.OnUse();

            owner.animationHandler.CrossFadePlay(_AttackTopData, 0.1f);
            
            if(owner.movement.velocity.magnitude <= 0.001f)
                owner.animationHandler.CrossFadePlay(_AttackBottomData, 0.1f);
        }

        public override void End()
        {
            base.End();
            
            owner.animationHandler.Stop(_AttackTopData);
            owner.animationHandler.Stop(_AttackBottomData);
        }
    }
}