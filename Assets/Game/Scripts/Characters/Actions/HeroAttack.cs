using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;

namespace Game.Characters.Actions
{
    public abstract class HeroAttack<T> : HeroCoolDownAction<T>, IHeroAttackAction where T : Hero 
    {
        [SerializeField]
        float _Damage;
        [SerializeField]
        float _Range;
        [SerializeField]
        float _Speed;
        [SerializeField]
        float _ManaGainOnHit;

        bool _IsRestricted;

        public const string SpeedProperty = "speed";
        public const string RangeProperty = "range";
        public const string DamageProperty = "damage";
        public const string ManaGainOnHitProperty = "manaGainOnHit";

        public float manaGainOnHit => _ManaGainOnHit;
        public float range => _Range;
        public float speed => _Speed;
        public float damage => _Damage;

        public bool isRestricted => _IsRestricted;

        protected override void Awake()
        {
            base.Awake();

            SetPropertyStartValue(SpeedProperty, _Speed);
            SetPropertyStartValue(RangeProperty, _Range);
            SetPropertyStartValue(DamageProperty, _Damage);
            SetPropertyStartValue(ManaGainOnHitProperty, _ManaGainOnHit);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted;
        }

        public override void Upgrade(string property, object value)
        {
            if(!Upgrades.Utilities.DebugAssertProperty(this, property))
                return;

            switch(property)
            {
                case SpeedProperty:
                    if(value is float s)
                        _Speed = s;
                    break;
                case RangeProperty:
                    if(value is float r)
                        _Range = r;
                    break;
                case DamageProperty:
                    if(value is float d)
                        _Damage = d;
                    break;
                case ManaGainOnHitProperty:
                    if(value is float m)
                        _ManaGainOnHit = m;
                    break;
            }
        }

        public override bool Contains(string property)
        {
            return property switch
            {
                SpeedProperty => true,
                RangeProperty => true,
                DamageProperty => true,
                ManaGainOnHitProperty => true,
                _ => false,
            };
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}