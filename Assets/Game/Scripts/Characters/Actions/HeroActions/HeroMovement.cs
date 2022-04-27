using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using System;

namespace Game.Characters.Actions
{
    public abstract class HeroMovement<T> : Movement<T>, IHeroMovementAction where T : Hero
    {
        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();

        public event Action<IProperty, float> onUpgradePropertyEvent;
        public event Action<IProperty> onRevertPropertyEvent;

        public void Upgrade(string property, float value)
        {    
            if(!ContainsProperty(property))
            {
                Debug.LogAssertion($"[Upgrade Error] {property} does not exist in {this}!");
                
                return;
            }

            IProperty p = GetProperty(property);

            if(!_UpgradedProperties.ContainsKey(p))
                _UpgradedProperties.Add(p, 0);

            float difference = value - _UpgradedProperties[p];
                
            _UpgradedProperties[p] += difference;

            p.valueAdded += difference;

            onUpgradePropertyEvent?.Invoke(p, difference);
        }

        public void Revert(string property)
        {
            if(!ContainsProperty(property))
            {
                Debug.LogAssertion($"[Upgrade Error] {property} does not exist in {this}!");
                
                return;
            }

            IProperty p = GetProperty(property);

            p.valueAdded -= _UpgradedProperties[p];

            _UpgradedProperties[p] = 0;

            onRevertPropertyEvent?.Invoke(p);
        }
    }
}