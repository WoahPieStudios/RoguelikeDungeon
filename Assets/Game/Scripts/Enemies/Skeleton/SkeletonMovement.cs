using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;
using Game.Animations;

namespace Game.Enemies.Skeleton
{
    public class SkeletonMovement : EnemyMovement<Skeleton>
    {
        [Header("Animation Clips")]
        [SerializeField]
        AnimationData _IdleData;
        [SerializeField]
        AnimationData _WalkData;

        [SerializeField]
        float _LerpTime;
        Vector2 _Velocity;
        Vector2 _Direction;

        bool _IsMoving = false;

        public override Vector2 velocity => _Velocity;

        protected override void Awake()
        {
            base.Awake();

            Use();
        }

        private void Start() 
        {
            owner.animationHandler.AddAnimationData(_IdleData);
            owner.animationHandler.AddAnimationData(_WalkData);    

            owner.animationHandler.Play(_IdleData);
        }

        private void FixedUpdate() 
        {
            if(isActive && !isRestricted)
            {
                _Velocity = Vector2.Lerp(velocity, _Direction * speed * Time.fixedDeltaTime, _LerpTime);

                transform.position += (Vector3)_Velocity;
            }

            if(_Velocity.magnitude > 0.05f && !_IsMoving)
            {
                _IsMoving = true;

                owner.animationHandler.CrossFadePlay(_WalkData, 0.05f);
            }

            else if(_Velocity.magnitude <= 0.05f && _IsMoving)
            {
                _IsMoving = false;

                owner.animationHandler.CrossFadePlay(_IdleData, 0.05f);
            }
        }

        public override bool Move(Vector2 direction)
        {
            bool canMove = base.Move(direction);

            if(canMove)
                _Direction = direction;

            return canMove;
        }
    }
}