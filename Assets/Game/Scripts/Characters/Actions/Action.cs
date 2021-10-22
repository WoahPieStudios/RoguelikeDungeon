using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Action<T> : ScriptableObject, ICopyable<T> where T : Object
    {
        bool _IsActive = false;

        public bool isActive => _IsActive;

        protected void Begin()
        {
            _IsActive = true;
        }

        public virtual void End()
        {
            _IsActive = false;
        }

        public abstract void Tick();

        public virtual T CreateCopy()
        {
            return Instantiate(this) as T;
        }
    }
}