using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Attack : Action
    {
        [SerializeField]
        int _Damage = 0;
        [SerializeField]
        float _Range = 0;
        [SerializeField]
        float _Speed = 0;
        [SerializeField]
        ActiveEffect[] _ActiveEffects; // Well its actually for Knockback but eh~

        protected int damage => _Damage;
        protected float range => _Range;
        protected float speed => _Speed;
        protected ActiveEffect[] activeEffects => _ActiveEffects;

        public virtual bool CanUse(CharacterBase attacker)
        {
            return !isActive;
        }

        // I didn't add the effects yet because I think you should add it yourself at your own time.
        public virtual void Use(CharacterBase attacker)
        {
            Begin(attacker);
        }
    }
}