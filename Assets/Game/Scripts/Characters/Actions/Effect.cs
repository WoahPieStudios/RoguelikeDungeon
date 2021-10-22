using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class Effect<T> : Action<Effect<T>>, IStackable
        where T : Character<T>
    {
        [SerializeField]
        RestrictAction _RestrictAction;

        Character<T> _Effected;

        public abstract bool isStackable { get; }

        public RestrictAction restrictAction => _RestrictAction;
        protected Character<T> effected => _Effected;

        public virtual void StartEffect(Character<T> effected)
        {
            _Effected = effected;

            Begin();
        }
    }
}