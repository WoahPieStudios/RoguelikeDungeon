using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Properties
{
    public interface IHealthProperty : IActorProperty
    {
        int maxHealth { get; }
        int currentHealth { get; }
        bool isAlive { get; }

        event Action<IHealthProperty, int> onAddHealthEvent;
        event Action<IHealthProperty, int> onDamageEvent;
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

    public interface IManaProperty : IActorProperty
    { 
        int maxMana { get; }
        int currentMana { get; }

        event Action<IManaProperty, int> onUseManaEvent;
        event Action<IManaProperty, int> onAddManaEvent;
        event Action onDrainManaEvent;
        event Action onResetManaEvent;

        void AddMana(int mana);
        void UseMana(int mana);
        void ResetMana();
        void DrainMana();
    }
}