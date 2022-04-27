using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;
using Game.Characters.Properties;

namespace Game.Characters
{
    public class Enemy : Character
    {
        [SerializeField]
        Health _Health;

        IAttackAction _Attack;

        IMovementAction _Movement; 

        public IAttackAction attack => _Attack;

        public IMovementAction movement => _Movement;

        public Health health => _Health;

        protected override void Awake()
        {
            base.Awake();

            _Health.SetCurrentHealthWithoutEvent(_Health.maxHealth);

            _Attack = GetComponent<IAttackAction>();
            _Movement = GetComponent<IMovementAction>();
        }
    }
}