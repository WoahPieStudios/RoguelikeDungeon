using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Properties;
using Game.Actions;

namespace Game.Characters.Actions
{
    public abstract class Attack<T> : CoolDownAction<T>, IAttackAction where T : Character
    {
        [SerializeField]
        Property _Damage = new Property(SpeedProperty);
        [SerializeField]
        Property _Range = new Property(RangeProperty);
        [SerializeField]
        Property _Speed = new Property(SpeedProperty);
        
        bool _IsRestricted = false;

        public Property speed => _Damage;

        public Property damage => _Range;

        public Property range => _Speed;

        public bool isRestricted => _IsRestricted;

        public const string SpeedProperty = "speed";
        public const string RangeProperty = "range";
        public const string DamageProperty = "damage";

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
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}