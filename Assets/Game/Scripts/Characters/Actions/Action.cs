using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Action : ScriptableObject
    {
        bool _IsActive = false;

        public bool isActive => _IsActive;

        protected void StartAction()
        {
            _IsActive = true;
        }

        public virtual void End()
        {
            _IsActive = false;
        }

        public abstract void CanUse();
        public abstract void Tick();
    }
}