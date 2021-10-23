using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters
{
    public abstract class CharacterBase : MonoBehaviour, IHealth, IMove, IEffectable, IDirectional
    {
        // Movement
        float _MoveSpeed = 0;
        Vector2 _Velocity;

        // Direction
        Vector2Int _FaceDirection;

        // Effects
        List<Effect> _EffectList = new List<Effect>();

       // Health
        public abstract int maxHealth { get; }
        public abstract int currentHealth { get; }
        public abstract bool isAlive { get; }

        // Effects
        public Effect[] effects => _EffectList.ToArray();

        // Movement
        public Vector2 velocity => _Velocity;

        // Direction
        public Vector2Int faceDirection => _FaceDirection;

        // Health
        public abstract void AddHealth(int health);

        public abstract void Damage(int damage);

        // Movement
        public virtual void Move(Vector2 direction)
        {
            _Velocity = direction * _MoveSpeed;

            transform.position += (Vector3)_Velocity * Time.fixedDeltaTime;
        }

         // Effects
        public virtual void AddEffects(params Effect[] effects)
        {
            Effect effect;

            foreach(Effect e in effects)
            {
                effect = e.CreateCopy();

                effect.StartEffect(this);

                _EffectList.Add(effect);
            }
        }

        public virtual void RemoveEffects(params Effect[] effects)
        {
            foreach(Effect effect in effects)
            {
                if(_EffectList.Contains(effect))
                    _EffectList.Remove(effect);
            }
        }

        // Direction
        public virtual void Orient(Vector2Int faceDirection) // JOLO
        {
            _FaceDirection = faceDirection;
        }
    }
}