using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public class Character<T> : MonoBehaviour
        where T : Character<T> 
    {
        // Health
        int _CurrentHealth = 0;

        // Movement
        float _MoveSpeed = 0;
        Vector2 _Velocity;
        
        // Effects
        List<Effect<T>> _EffectList;

        // Character Data
        T _Data;

        // Health
        public int maxHealth => _Data.maxHealth;
        public bool isAlive => _CurrentHealth > 0;

        // Movement
        public Vector2 velocity => _Velocity;

        // Effects
        public Effect<T>[] effects => _EffectList.ToArray();

        // Attack
        public int attackDamage => _Data.attackDamage;
        public float attackRange => _Data.attackRange;
        public Attack<T> attack => _Data.attack;
        
        // Character Data
        protected T data => _Data;

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
        public virtual void AddEffect(Effect<T> effect)
        {
            _EffectList.Add(effect);
        }

        public virtual void RemoveEffect(Effect<T> effect)
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
        protected virtual void AssignCharacter(T characterData)
        {
            _Data = characterData;
        } 
    }
}