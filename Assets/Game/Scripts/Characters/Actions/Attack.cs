using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Attack : Action
    {
        int _Damage = 0;
        float _Range = 0;

        protected int damage => _Damage;
        protected float range => _Range;

        public virtual void Use(int damage, float range)
        {
            _Damage = damage;

            _Range = range;

            StartAction();
        }
    }
}