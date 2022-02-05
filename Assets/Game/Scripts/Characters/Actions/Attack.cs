using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Effects;

namespace Game.Characters.Actions
{
    public abstract class Attack : CoolDownAction, IAttackAction
    {
        [SerializeField]
        int _Damage;
        [SerializeField]
        float _Range;
        [SerializeField]
        float _Speed;
        [SerializeField]
        int _ManaGainOnHit;
        bool _IsRestricted = false;

        /// <summary>
        /// Damage of the Attack.
        /// </summary>
        public int damage => _Damage;

        /// <summary>
        /// Range of the Attack
        /// </summary>
        public float range => _Range;

        /// <summary>
        /// Speed of the Attack. Honestly have no idea where this would fit.
        /// </summary>
        public float speed => _Speed;

        public int manaGainOnHit => _ManaGainOnHit;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onActionEvent;

        protected virtual void OnActionEvent(TrackActionType trackAction)
        {
            onActionEvent?.Invoke(trackAction);
        }

        protected override void Begin()
        {
            base.Begin();

            OnActionEvent(TrackActionType.Attack);
        }

        /// <summary>
        /// Starts Attack.
        /// </summary>
        /// <param name="attacker"></param>
        public virtual bool Use()
        {
            bool canUse = !isActive && !isRestricted;

            if(canUse)
                Begin();

            return canUse;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Attack);
        }
    }
}