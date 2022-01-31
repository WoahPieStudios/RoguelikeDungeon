using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actions
{
    public abstract class ActorAction : MonoBehaviour, IActorAction
    {
        bool _IsActive = false;

        IActor _Owner;

        public bool isActive => _IsActive;

        public virtual IActor owner => _Owner;

        protected virtual void Awake()
        {
            _Owner = GetComponent<IActor>();
        }

        protected virtual void Begin()
        {
            _IsActive = true;
        }

        public virtual void End()
        {
            _IsActive = false;
        }
    }
}