using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Character<TData> : SpriteCharacter
        where TData : CharacterData
    {
        // Attack
        Attack _Attack;
        // Health
        int _CurrentHealth = 0;

        // Character Data
        TData _Data;

        // Health
        /// <summary>
        /// Max Health of the Character
        /// </summary>
        public override int maxHealth => _Data.maxHealth;
        
        /// <summary>
        /// Current Health of the Character 
        /// </summary>
        public override int currentHealth => _CurrentHealth;

        /// <summary>
        /// Checks if the Character is Alive
        /// </summary>
        public override bool isAlive => _CurrentHealth > 0;

        // Move
        /// <summary>
        /// Move Speed of the Character
        /// </summary>
        public override float moveSpeed => _Data.moveSpeed;

        // Attack
        /// <summary>
        /// Attack of the Character
        /// </summary>
        public override Attack attack => _Attack;
        
        // Character Data
        /// <summary>
        /// Data of the Character
        /// </summary>
        public TData data => _Data;

        // Health
        /// <summary>
        /// Adds to the Health of the Character
        /// </summary>
        /// <param name="health">Amount to be added</param>
        public override void AddHealth(int health)
        {
            int newHealth = _CurrentHealth + health;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        }

        /// <summary>
        /// Reduces the Health of the Character
        /// </summary>
        /// <param name="damage">Amount to be reduced the health by</param>
        public override void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth < 0 ? 0 : newHealth;
        }
        
        // Character Data
        /// <summary>
        /// Assigns the Data of the Character and setups up their corresponding variables.
        /// </summary>
        /// <param name="data">Data of the Character</param>
        public virtual void AssignData(TData data)
        {
            _Data = data;

            _CurrentHealth = maxHealth;

            _Attack = data.attack?.CreateCopy<Attack>();
            _Attack.IsCast<IOnAssignEvent>()?.OnAssign(this);
        }

        // Attack
        /// <summary>
        /// Starts the Attack.
        /// </summary>
        /// <returns>if the Attack is used</returns>
        public override bool UseAttack()
        {
            bool canAttack = attack && attack.CanUse(this) && !restrictedActions.HasFlag(RestrictAction.Attack);

            if(canAttack)
                attack.Use(this);

            return canAttack;
        }
    }
}