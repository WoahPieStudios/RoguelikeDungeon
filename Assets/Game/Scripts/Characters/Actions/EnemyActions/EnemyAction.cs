using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;

namespace Game.Characters.Actions
{
    public class EnemyAction<T> : Action, ICharacterAction<T> where T : Enemy
    {
        T _Owner;

        public T owner => _Owner;

        protected override void Awake()
        {
            base.Awake();

            _Owner = GetComponent<T>();
        }
    }
}