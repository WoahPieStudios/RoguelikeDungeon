using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;
using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate<T> : CoolDownAction<T>, IUltimateAction where T : Hero
    {
        [SerializeField]
        Property _ManaCost = new Property(ManaCostProperty);

        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();
        
        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimatex
        /// </summary>
        public Property manaCost => _ManaCost;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onUseTrackableAction;
        public event Action<IProperty, float> onUpgradePropertyEvent;
        public event Action<IProperty> onRevertPropertyEvent;

        public const string ManaCostProperty = "manaCost";

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(manaCost);
        }

        protected override void OnUse()
        {
            base.OnUse();

            Mana mana = owner.mana;

            mana.ReduceValue(manaCost);

            onUseTrackableAction?.Invoke(TrackActionType.Ultimate);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted && !isCoolingDown && owner.mana.currentValue >= manaCost;
        }
        
        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Ultimate);
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