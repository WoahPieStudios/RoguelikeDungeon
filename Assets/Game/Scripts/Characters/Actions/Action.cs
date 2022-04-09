using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;

namespace Game.Characters.Actions
{
    [RequireComponent(typeof(AnimationHandler))]
    public abstract class Action<T> : MonoBehaviour, IAction where T : Character
    {
        bool _IsActive = false;

        T _Owner;

        AnimationHandler _AnimationHandler;

        public bool isActive => _IsActive;
        public AnimationHandler animationHandler => _AnimationHandler;
        public T owner => _Owner;

        protected virtual void Awake()
        {
            _AnimationHandler = GetComponent<AnimationHandler>();

            _Owner = GetComponent<T>();
        }

        protected virtual void OnUse()
        {
            _IsActive = true;
        }

        protected virtual bool CanUse()
        {
            return !_IsActive;
        }

        public void Use()
        {
            if(CanUse())
                OnUse();
        }

        public virtual void End()
        {
            _IsActive = false;
        }
    }
}