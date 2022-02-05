using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Properties
{
    public interface IHealth
    {
        int maxHealth { get; }
        int currentHealth { get; }
        bool isAlive { get; }

        event Action<IHealth, int> onAddHealthEvent;
        event Action<IHealth, int> onDamageEvent;
        event Action onKillEvent;
        event Action onResetHealthEvent;
        
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        void AddHealth(int addHealth);

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        void Damage(int damage);
        void Kill();
        void ResetHealth();
    }

    public interface IMana
    { 
        int maxMana { get; }
        int currentMana { get; }

        event Action<IMana, int> onUseManaEvent;
        event Action<IMana, int> onAddManaEvent;
        event Action onDrainManaEvent;
        event Action onResetManaEvent;

        void AddMana(int mana);
        void UseMana(int mana);
        void ResetMana();
        void DrainMana();
    }
}