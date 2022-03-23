using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;

namespace Game.Enemies.Skeleton
{
    public class SkeletonAttack : Attack
    {
        [SerializeField]
        private AnimationData _AttackTopData;
        [SerializeField]
        private AnimationData _AttackBottomData;

        private AnimationHandler _AnimationHandler;

        protected override void Awake()
        {
            base.Awake();

            _AnimationHandler = GetComponent<AnimationHandler>();
        }

        private void Start() 
        {
            _AnimationHandler.AddAnimationData(_AttackTopData, End);
            _AnimationHandler.AddAnimationData(_AttackBottomData, End);
        }

        private void Update() 
        {
            if(!isActive)
                return;
                
            if((owner as Skeleton).movement.velocity.magnitude > 0.001f)
            {
                _AnimationHandler.Stop(_AttackBottomData);
            }

            else if(!_AnimationHandler.IsAnimationPlaying(_AttackBottomData))
            {
                _AnimationHandler.Play(_AttackBottomData);
                _AnimationHandler.SyncAnimations(_AttackTopData, _AttackBottomData);
            }
        }

        protected override void Begin()
        {
            base.Begin();

            _AnimationHandler.CrossFadePlay(_AttackTopData, 0.1f);
            
            if((owner as Skeleton).movement.velocity.magnitude <= 0.001f)
                _AnimationHandler.CrossFadePlay(_AttackBottomData, 0.1f);
        }

        public override void End()
        {
            base.End();
            
            _AnimationHandler.Stop(_AttackTopData);
            _AnimationHandler.Stop(_AttackBottomData);
        }
    }
}