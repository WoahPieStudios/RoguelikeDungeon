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
        ActiveEffect[] _Effects;

        protected int damage => _Damage;
        protected float range => _Range;
        protected float speed => _Speed;
        protected ActiveEffect[] effects => _Effects;

        public abstract bool CanUse();

        public virtual void Use(CharacterBase attacker)
        {
            Begin(attacker);
        }
    }
}