using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Character<TData> : CharacterBase
        where TData : CharacterData
    {
        // Health
        int _CurrentHealth = 0;

        // Character Data
        TData _Data;

        // Health
        public override int maxHealth => _Data.maxHealth;
        public override int currentHealth => _CurrentHealth;
        public override bool isAlive => _CurrentHealth > 0;

        // Move
        public override float moveSpeed => _Data.moveSpeed;

        // Attack
        public override Attack attack => _Data.attack;
        
        // Character Data
        public TData data => _Data;

        // Health
        public override void AddHealth(int health)
        {
            int newHealth = _CurrentHealth + health;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        }

        public override void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth < 0 ? 0 : newHealth;
        }
        
        // Character Data
        public virtual void AssignData(TData data)
        {
            _Data = data;

            _CurrentHealth = maxHealth;
        }

        // Attack
        public override bool UseAttack()
        {
            bool canAttack = attack && attack.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Attack);

            if(canAttack)
                attack.Use(this);

            return canAttack;
        }
    }
}