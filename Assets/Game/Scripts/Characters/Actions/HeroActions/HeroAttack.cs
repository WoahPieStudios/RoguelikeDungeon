using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;
using System;

namespace Game.Characters.Actions
{
    public abstract class HeroAttack<T> : HeroCoolDownAction<T>, IHeroAttackAction where T : Hero 
    {
        [SerializeField]
        Property _Damage = new Property(SpeedProperty);
        [SerializeField]
        Property _Range = new Property(RangeProperty);
        [SerializeField]
        Property _Speed = new Property(DamageProperty);
        [SerializeField]
        Property _ManaGainOnHit = new Property(ManaGainOnHitProperty);

        bool _IsRestricted;

        public const string SpeedProperty = "speed";
        public const string RangeProperty = "range";
        public const string DamageProperty = "damage";
        public const string ManaGainOnHitProperty = "manaGainOnHit";

        public event Action<TrackActionType> onUseTrackableAction;

        public Property manaGainOnHit => _ManaGainOnHit;
        public Property range => _Range;
        public Property speed => _Speed;
        public Property damage => _Damage;

        public bool isRestricted => _IsRestricted;

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

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}