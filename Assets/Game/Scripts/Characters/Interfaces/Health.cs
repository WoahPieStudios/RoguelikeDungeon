using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    public interface IKill
    {
        event System.Action onKillEvent;

        void Kill();
    }

    public interface IResetHealth
    {
        event System.Action onResetHealthEvent;

        void ResetHealth();
    }

    // Interface for not forgetting Health
    public interface IHealth : IKill, IResetHealth
    {
        int maxHealth { get; }
        int currentHealth { get; }
        bool isAlive { get; }

        event System.Action<IHealth, int> onAddHealthEvent;
        event System.Action<IHealth, int> onDamageEvent;

        void AddHealth(int health);
        void Damage(int damage);
    }
}