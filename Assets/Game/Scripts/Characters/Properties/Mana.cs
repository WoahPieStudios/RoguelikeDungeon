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
        Property _MaxMana = new Property(maxManaProperty);
        [SerializeField]
        float _CurrentMana = 0;

        List<IProperty> _PropertyList = new List<IProperty>();

        public event Action<Mana, float> onUseManaEvent;
        public event Action<Mana, float> onAddManaEvent;
        public event Action<Mana> onDrainManaEvent;
        public event Action<Mana> onResetManaEvent;
        public event Action<Mana> onNewMaxManaEvent;

        public Property maxMana => _MaxMana;

        public float currentMana => _CurrentMana;

        public IProperty[] properties => _PropertyList.ToArray();

        public const string maxManaProperty = "maxMana";

        public Mana()
        {
            _PropertyList.Add(maxMana);
        }

        public void SetMaxManaWithoutEvent(float newMaxMana)
        {
            _MaxMana.startValue = newMaxMana > 0 ? newMaxMana : 0;
        }

        public void SetMaxMana(float newMaxMana)
        {
            SetMaxManaWithoutEvent(newMaxMana);

            onNewMaxManaEvent?.Invoke(this);
        }
        

        public void SetManaWithoutNotify(float newMana)
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
            SetManaWithoutNotify(_CurrentMana + mana);

            onAddManaEvent?.Invoke(this, mana);
        }

        public void UseMana(float mana)
        {
            SetManaWithoutNotify(_CurrentMana - mana);

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
    }
}