using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;

namespace Game.Characters.Actions
{
    public abstract class HeroMovement<T> : Movement<T>, IUpgradeable where T : Hero
    {
        [SerializeField]
        float _Speed;

        public override float speed => _Speed;

        Dictionary<string, object> _PropertiesStartValues = new Dictionary<string, object>();

        public const string SpeedProperty = "speed";

        protected override void Awake()
        {
            base.Awake();

            SetPropertyStartValue(SpeedProperty, _Speed);
        }

        protected void SetPropertyStartValue(string property, object value)
        {
            if(_PropertiesStartValues.ContainsKey(property))
                _PropertiesStartValues[property] = value;

            else
                _PropertiesStartValues.Add(property, value);
        }

        public virtual void Upgrade(string property, object value)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;

            switch(property)
            {
                case SpeedProperty:
                    if(value is float v)
                        _Speed = v;
                    break;
            }
        }

        public virtual object GetStartValue(string property)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return null;

            return _PropertiesStartValues[property];
        }

        public virtual void Revert(string property)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;
                
            Upgrade(property, GetStartValue(property));
        }

        public virtual bool Contains(string property)
        {
            return property switch
            {
                SpeedProperty => true,
                _ => false,
            };
        }

        public override bool Move(Vector2 direction)
        {
            return base.Move(direction) && !isRestricted;
        }
    }
}