using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using System;

namespace Game.Characters.Actions
{
    public abstract class HeroAttack<T> : Attack<T>, IHeroAttackAction where T : Hero 
    {
        [SerializeField]
        Property _ManaGainOnHit = new Property(ManaGainOnHitProperty);

        public const string ManaGainOnHitProperty = "manaGainOnHit";

        public event Action<TrackActionType> onUseTrackableAction;

        public Property manaGainOnHit => _ManaGainOnHit;

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(manaGainOnHit);
            propertyList.Add(range);
            propertyList.Add(speed);
            propertyList.Add(damage);
        }

        protected override void OnUse()
        {
            base.OnUse();

            onUseTrackableAction?.Invoke(TrackActionType.Attack);
        }
    }
}