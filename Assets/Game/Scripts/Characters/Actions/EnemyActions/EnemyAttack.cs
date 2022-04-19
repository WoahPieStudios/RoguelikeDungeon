using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class EnemyAttack<T> : EnemyCoolDownAction<T>, IAttackAction where T : Enemy
    {
        bool _IsRestricted = false;

        public abstract Property speed  { get; }

        public abstract Property damage { get; }

        public abstract Property range { get; }

        public bool isRestricted => _IsRestricted;

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(damage);
            propertyList.Add(range);
            propertyList.Add(speed);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            throw new System.NotImplementedException();
        }
    }
}