using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Properties
{
    [Serializable]
    public class Health : IHealth, IActorProperty
    {
        [SerializeField]
        int _MaxHealth = 0;
        [SerializeField]
        int _CurrentHealth = 0;

        public event Action<IHealth, int> onAddHealthEvent;
        public event Action<IHealth, int> onDamageEvent;
        public event Action onKillEvent;
        public event Action onResetHealthEvent;

        public int maxHealth => _MaxHealth;
        public int currentHealth => _CurrentHealth;

        public bool isAlive => _CurrentHealth > 0;

        public IActor owner { get; set; }
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public void AddHealth(int addHealth)
        {
            int newHealth = _CurrentHealth + addHealth;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;

            onAddHealthEvent?.Invoke(this, addHealth);
        }

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth < 0 ? 0 : newHealth; 

            onDamageEvent?.Invoke(this, damage);
        }

        public void Kill()
        {
            _CurrentHealth = 0;

            onKillEvent?.Invoke();
        }
        
        public void ResetHealth()
        {
            _CurrentHealth = _MaxHealth;

            onResetHealthEvent?.Invoke();
        }
    }
}