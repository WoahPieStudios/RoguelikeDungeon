using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Properties;
using Game.Upgrades;
using Game.Actions;

namespace Game.Characters.Actions
{
    public class HeroAction<T> : Action, ICharacterAction<T>, IUpgradeable where T : Hero
    {
        T _Owner;

        Dictionary<IProperty, float> _PropertyUpgrades => new Dictionary<IProperty, float>();

        public T owner => _Owner;

        protected override void Awake()
        {
            base.Awake();

            _Owner = GetComponent<T>();
        }

        protected virtual void Start()
        {
            foreach(IProperty property in properties)
                _PropertyUpgrades.Add(property, 0);
        }

        public void Upgrade(string property, float value)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;

            IProperty p = GetProperty(property);

            float difference = value - _PropertyUpgrades[p];
                
            _PropertyUpgrades[p] += difference;

            p.valueAdded += difference;
        }

        public void Revert(string property)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;

            IProperty p = GetProperty(property);

            p.valueAdded -= _PropertyUpgrades[p];

            _PropertyUpgrades[p] = 0;
        }
    }
}