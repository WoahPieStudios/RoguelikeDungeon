using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Attack<TCharacter> : Action<Attack<TCharacter>>
        where TCharacter : Character<TCharacter>
    {
        int _Damage = 0;
        float _Range = 0;

        protected int damage => _Damage;
        protected float range => _Range;

        public abstract bool CanUse();

        public virtual void Use(TCharacter attacker, int damage, float range)
        {
            _Damage = damage;

            _Range = range;

            Begin();
        }
    }
}