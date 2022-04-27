using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;
using Game.Upgrades;

namespace Game.Characters.Actions
{
    public abstract class Skill<T> : CoolDownAction<T>, ISkillAction where T : Hero
    {
        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();

        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onUseTrackableAction;
        public event Action<IProperty, float> onUpgradePropertyEvent;
        public event Action<IProperty> onRevertPropertyEvent;

        protected override void OnUse()
        {
            base.OnUse();
            
            onUseTrackableAction?.Invoke(TrackActionType.Skill);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted;
        }
        
        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }

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