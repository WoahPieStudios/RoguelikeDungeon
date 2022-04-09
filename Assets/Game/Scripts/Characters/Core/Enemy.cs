using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters
{
    public class Enemy : Character
    {
        IAttackAction _Attack;

        public IAttackAction attack => _Attack;

        protected override void Awake()
        {
            base.Awake();

            _Attack = GetComponent<IAttackAction>();
        }
    }
}