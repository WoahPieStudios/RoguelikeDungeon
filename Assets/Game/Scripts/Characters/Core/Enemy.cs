using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters
{
    public class Enemy : Character
    {
        IAttackAction _Attack;

        IMovementAction _Movement; 

        public IAttackAction attack => _Attack;

        public IMovementAction movement => _Movement;

        protected override void Awake()
        {
            base.Awake();

            _Attack = GetComponent<IAttackAction>();
            _Movement = GetComponent<IMovementAction>();
        }
    }
}