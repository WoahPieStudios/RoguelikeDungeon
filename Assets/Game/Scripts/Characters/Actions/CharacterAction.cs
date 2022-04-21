using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Actions
{
    public class CharacterAction<T> : Action where T : Character
    {
        T _Owner;

        public T owner => _Owner;

        protected virtual void Awake()
        {
            _Owner = GetComponent<T>();
        }
    }
}