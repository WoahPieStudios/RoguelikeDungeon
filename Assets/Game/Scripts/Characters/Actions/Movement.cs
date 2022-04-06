using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;

namespace Game.Characters.Actions
{
    public abstract class Movement : Action<Character>, IUpgradeable
    {
        [SerializeField]
        float _Speed;
        
        bool _IsRestricted = false;

        public float speed => _Speed;

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

        public virtual bool Move(Vector2 direction)
        {
            return isActive && !isRestricted;
        }

        public virtual void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Movement);
        }

        public virtual bool Use()
        {
            if(!isActive)
                Begin();

            return isActive;
        }

        public virtual void Upgrade(string property, object value)
        {
            switch(property)
            {
                case "speed":
                    if(value is float v)
                        _Speed = v;
                    break;
            }
        }
    }
}