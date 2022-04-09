using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Effects;

namespace Game.Characters.Actions
{
    public abstract class Attack<T> : CoolDownAction<T>, IAttackAction where T : Character
    {
        bool _IsRestricted = false;

        /// <summary>
        /// Damage of the Attack.
        /// </summary>
        public abstract float damage { get; }

        /// <summary>
        /// Range of the Attack
        /// </summary>
        public abstract float range { get; }

        /// <summary>
        /// Speed of the Attack. Honestly have no idea where this would fit.
        /// </summary>
        public abstract float speed { get; }

        public bool isRestricted => _IsRestricted;

        public event System.Action<TrackActionType> onUseTrackableAction;

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
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Attack);
        }
    }
}