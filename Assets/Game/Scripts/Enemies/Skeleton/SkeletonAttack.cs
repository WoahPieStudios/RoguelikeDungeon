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
        Property _Damage;
        [SerializeField]
        Property _Range;
        [SerializeField]
        Property _Speed;
        [SerializeField]
        Property _CoolDownTime;
        [SerializeField]
        private AnimationData _AttackTopData;
        [SerializeField]
        private AnimationData _AttackBottomData;

        public override Property damage => _Damage;

        public override Property range => _Range;

        public override Property speed => _Speed;

        public override Property coolDownTime => _CoolDownTime;

        private void Start() 
        {
            animationHandler.AddAnimationData(_AttackTopData, End);
            animationHandler.AddAnimationData(_AttackBottomData, End);
        }

        private void Update() 
        {
            if(!isActive || isRestricted)
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

        protected override void OnUse()
        {
            base.OnUse();

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