using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    [Serializable]
    public class Health
    {
        [SerializeField]
        int _MaxHealth = 0;

        int _CurrentHealth = 0;

        public int maxHealth => _MaxHealth;

        public int currentHealth => _CurrentHealth;

        public bool isAlive => _CurrentHealth > 0;

        public Health()
        {
            ResetHealth();
        }
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public void AddHealth(int addHealth)
        {
            int newHealth = _CurrentHealth + addHealth;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        }

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth < 0 ? 0 : newHealth; 
        }

        public void Kill()
        {
            _CurrentHealth = 0;
        }

        public void ResetHealth()
        {
            _CurrentHealth = _MaxHealth;
        }
    }
}