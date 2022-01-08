using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        int _MaxHealth = 0;
        int _CurrentHealth = 0;

        public int maxHealth => _MaxHealth;

        public int currentHealth => _CurrentHealth;

        public bool isAlive => _CurrentHealth > 0;

        void Awake() 
        {
            ResetHealth();
        }
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public virtual void AddHealth(int addHealth)
        {
            int newHealth = _CurrentHealth + addHealth;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        }

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public virtual void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth < 0 ? 0 : newHealth; 
        }

        public virtual void Kill()
        {
            _CurrentHealth = 0;
        }

        public virtual void ResetHealth()
        {
            _CurrentHealth = _MaxHealth;
        }
    }
}