using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters;
using Game.Characters.Actions;
using Game.Animations;

namespace Game.Enemies.Skeleton
{
    public class SkeletonMovement : Movement
    {
        [Header("Animation Clips")]
        [SerializeField]
        AnimationClip _IdleClip;
        [SerializeField]
        AnimationClip _WalkClip;

        [SerializeField]
        float _LerpTime;
        Vector2 _Velocity;
        Vector2 _Direction;

        AnimationHandler _AnimationHandler;

        const string _IdleAnimName = "Idle"; 
        const string _WalkAnimName = "Walk"; 

        bool _IsMoving = false;

        public override Vector2 velocity => _Velocity;

        protected override void Awake()
        {
            base.Awake();

            _AnimationHandler = GetComponent<AnimationHandler>();

            Use();
        }

        private void Start() 
        {
            _AnimationHandler.AddAnimation(_IdleAnimName, _IdleClip, 0);
            _AnimationHandler.AddAnimation(_WalkAnimName, _WalkClip, 0);    
        }

        void FixedUpdate() 
        {
            if(isActive && !isRestricted)
            {
                _Velocity = Vector2.Lerp(velocity, _Direction * speed * Time.fixedDeltaTime, _LerpTime);

                transform.position += (Vector3)_Velocity;
            }

            if(_Velocity.magnitude > 0.05f && !_IsMoving)
            {
                _IsMoving = true;

                _AnimationHandler.CrossFadePlay(_WalkAnimName, 0.05f);
            }

            else if(_Velocity.magnitude <= 0.05f && _IsMoving)
            {
                _IsMoving = false;

                _AnimationHandler.CrossFadePlay(_IdleAnimName, 0.05f);
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