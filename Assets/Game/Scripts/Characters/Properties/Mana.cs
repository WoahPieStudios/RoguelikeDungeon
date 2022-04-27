using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;

namespace Game.Characters.Properties
{
    [Serializable]
    public class Mana : IUpgradeable
    {
        [SerializeField]
        Property _MaxMana = new Property(MaxManaProperty);
        [SerializeField]
        float _CurrentMana = 0;

        List<IProperty> _PropertyList = new List<IProperty>();

        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();

        public event Action<Mana, float> onUseManaEvent;
        public event Action<Mana, float> onAddManaEvent;
        public event Action<Mana> onDrainManaEvent;
        public event Action<Mana> onResetManaEvent;
        public event Action<IProperty, float> onUpgradePropertyEvent;
        public event Action<IProperty> onRevertPropertyEvent;

        public float maxMana => _MaxMana;

        public float currentMana => _CurrentMana;

        public IProperty[] properties => _PropertyList.ToArray();

        public const string MaxManaProperty = "maxMana";

        public Mana()
        {
            _PropertyList.Add(_MaxMana);
        }

        public void SetCurrentManaWithoutNotify(float newMana)
        {
            if(newMana < 0)
                _CurrentMana = 0;

            else if(newMana > maxMana)
                _CurrentMana = maxMana;
            
            else
                _CurrentMana = newMana;
        }

        public void AddMana(float mana)
        {
            SetCurrentManaWithoutNotify(_CurrentMana + mana);

            onAddManaEvent?.Invoke(this, mana);
        }

        public void UseMana(float mana)
        {
            SetCurrentManaWithoutNotify(_CurrentMana - mana);

            onUseManaEvent?.Invoke(this, mana);
        }

        public void ResetMana()
        {
            _CurrentMana = _MaxMana;

            onResetManaEvent?.Invoke(this);
        }

        public void DrainMana()
        {
            _CurrentMana = 0;

            onDrainManaEvent?.Invoke(this);
        }

        public bool ContainsProperty(string property)
        {
            return _PropertyList.Any(p => p.name == property);
        }

        public IProperty GetProperty(string property)
        {
            return _PropertyList.FirstOrDefault(p => p.name == property);
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