using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;

namespace Game.Characters.Actions
{
    public class HeroCoolDownAction<T> : CoolDownAction<T>, IUpgradeable where T : Hero
    {
        [SerializeField]
        float _CoolDownTime;

        Dictionary<string, object> _PropertiesStartValues = new Dictionary<string, object>();

        public override float coolDownTime => _CoolDownTime;

        public const string CoolDownTimeProperty = "coolDownTime";

        protected override void Awake()
        {
            base.Awake();
            
            SetPropertyStartValue(CoolDownTimeProperty, _CoolDownTime);
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
                case CoolDownTimeProperty:
                    if(value is float c)
                        _CoolDownTime = c;
                    break;
            }
        }

        public virtual object GetStartValue(string property)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return null;

            return _PropertiesStartValues[property];
        }

        public void Revert(string property)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;
                
            Upgrade(property, GetStartValue(property));
        }

        public virtual bool Contains(string property)
        {
            return property switch
            {
                CoolDownTimeProperty => true,
                _ => false,
            };
        }
    }
}