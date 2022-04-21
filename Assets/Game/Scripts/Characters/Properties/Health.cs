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
    public class Health : IUpgradeable
    {
        [SerializeField]
        Property _MaxHealth = new Property(maxHealthProperty);
        [SerializeField]
        float _CurrentHealth = 0;

        List<IProperty> _PropertyList = new List<IProperty>();

        public event Action<Health, float> onAddHealthEvent;
        public event Action<Health, float> onDamageEvent;
        public event Action<Health> onKillEvent;
        public event Action<Health> onResetHealthEvent;
        public event Action<Health> onNewMaxHealthEvent;

        public Property maxHealth => _MaxHealth;
        public float currentHealth => _CurrentHealth;

        public bool isAlive => _CurrentHealth > 0;

        public IProperty[] properties => _PropertyList.ToArray();

        public const string maxHealthProperty = "maxHealth";

        public Health()
        {
            _PropertyList.Add(maxHealth);
        }

        public void SetCurrentHealthWithoutEvent(float newHealth)
        {
            if(newHealth < 0)
                _CurrentHealth = 0;

            else if(newHealth > maxHealth)
                _CurrentHealth = maxHealth;
            
            else
                _CurrentHealth = newHealth;
        }

        public void SetMaxHealthWithoutEvent(float newMaxHealth)
        {
            _MaxHealth.startValue = newMaxHealth > 0 ? newMaxHealth : 0;
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            SetMaxHealthWithoutEvent(newMaxHealth);

            onNewMaxHealthEvent?.Invoke(this);
        }
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public void AddHealth(float addHealth)
        {
            SetCurrentHealthWithoutEvent(_CurrentHealth + addHealth);

            onAddHealthEvent?.Invoke(this, addHealth);
        }

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public void Damage(float damage)
        {
            SetCurrentHealthWithoutEvent(_CurrentHealth - damage);
            
            onDamageEvent?.Invoke(this, damage);
        }

        public void Kill()
        {
            _CurrentHealth = 0;

            onKillEvent?.Invoke(this);
        }
        
        public void ResetHealth()
        {
            _CurrentHealth = _MaxHealth;

            onResetHealthEvent?.Invoke(this);
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