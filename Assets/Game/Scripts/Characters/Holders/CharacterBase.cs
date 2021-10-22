using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class CharacterBase : MonoBehaviour
    {
        // Health
        int _CurrentHealth = 0;

        // Movement
        float _MoveSpeed = 0;
        Vector2 _Velocity;
        
       
        public abstract int maxHealth { get; }
        public bool isAlive => _CurrentHealth > 0;

        // Movement
        public Vector2 velocity => _Velocity;

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
    }
}