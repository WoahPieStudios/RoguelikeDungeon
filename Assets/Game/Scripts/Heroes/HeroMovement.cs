using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Characters.Actions;

namespace Game.Heroes
{
    public class HeroMovement : Movement
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

        AnimationHandler _AnimationHandler;

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
            _AnimationHandler.AddAnimationData(_IdleData);
            _AnimationHandler.AddAnimationData(_WalkData);    

            _AnimationHandler.Play(_IdleData);
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

                _AnimationHandler.CrossFadePlay(_WalkData, 0.05f);
            }

            else if(_Velocity.magnitude <= 0.05f && _IsMoving)
            {
                _IsMoving = false;

                _AnimationHandler.CrossFadePlay(_IdleData, 0.05f);
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