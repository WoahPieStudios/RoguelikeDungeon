using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Character : MonoBehaviour
    {
        // Health
        int _CurrentHealth = 0;

        // Movement
        float _MoveSpeed = 0;
        Vector2 _Velocity;
        
        // Effects
        List<Effect> _EffectList;

        // Character Data
        CharacterData _CharacterData;

        // Health
        public int maxHealth => _CharacterData.maxHealth;
        public bool isAlive => _CurrentHealth > 0;

        // Movement
        public Vector2 velocity => _Velocity;

        // Effects
        public Effect[] effects => _EffectList.ToArray();

        // Attack
        public int attackDamage => _CharacterData.attackDamage;
        public float attackRange => _CharacterData.attackRange;
        public Attack attack => _CharacterData.attack;
        
        // Character Data
        protected CharacterData characterData => _CharacterData;

        // Health
        public virtual void AddHealth(int health)
        {
            int newHealth = _CurrentHealth + health;

            _CurrentHealth = newHealth > maxHealth ? maxHealth : newHealth;
        }

        // Movement
        public virtual void Move(Vector2 direction)
        {
            _Velocity = direction * _MoveSpeed;

            transform.position += (Vector3)_Velocity * Time.fixedDeltaTime;
        }

        // Effects
        public virtual void AddEffect(Effect effect)
        {
            _EffectList.Add(effect);
        }

        public virtual void RemoveEffect(Effect effect)
        {
            if(_EffectList.Contains(effect))
                _EffectList.Remove(effect);
        }

        // Attack
        public virtual void Damage(int damage)
        {
            int newHealth = _CurrentHealth - damage;

            _CurrentHealth = newHealth > 0 ? newHealth : 0;            
        }

        // Character Data
        protected virtual void AssignCharacter(CharacterData characterData)
        {
            _CharacterData = characterData;
        } 
    }
}