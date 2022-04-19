using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class HeroMovement<T> : HeroAction<T>, IMovementAction where T : Hero
    {
        [SerializeField]
        Property _Speed = new Property(SpeedProperty);

        bool _IsRestricted = false;

        public Property speed => _Speed;

        public abstract Vector2 velocity { get; }

        public bool isRestricted => _IsRestricted;

        public const string SpeedProperty = "speed";

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(speed);
        }

        public virtual bool Move(Vector2 direction)
        {
            return isActive && !isRestricted;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Movement);
        }
    }
}