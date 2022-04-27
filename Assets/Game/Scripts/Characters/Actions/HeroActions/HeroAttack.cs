using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using System;

namespace Game.Characters.Actions
{
    public abstract class HeroAttack<T> : Attack<T>, IHeroAttackAction where T : Hero 
    {
        [SerializeField]
        Property _ManaGainOnHit = new Property(ManaGainOnHitProperty);

        public const string ManaGainOnHitProperty = "manaGainOnHit";

        public event Action<TrackActionType> onUseTrackableAction;
        public event Action<IProperty, float> onUpgradePropertyEvent;
        public event Action<IProperty> onRevertPropertyEvent;

        public Property manaGainOnHit => _ManaGainOnHit;

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(manaGainOnHit);
        }

        protected override void OnUse()
        {
            base.OnUse();

            onUseTrackableAction?.Invoke(TrackActionType.Attack);
        }

        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();


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