using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class Attack : CoolDownAction, ITrackableAction, IRestrictableAction
    {
        [SerializeField]
        int _Damage = 0;
        [SerializeField]
        float _Range = 0;
        [SerializeField]
        float _Speed = 0;
        [SerializeField]
        ActiveEffect[] _ActiveEffects;

        bool _IsRestricted = false;

        /// <summary>
        /// Damage of the Attack.
        /// </summary>
        protected int damage => _Damage;

        /// <summary>
        /// Range of the Attack
        /// </summary>
        protected float range => _Range;

        /// <summary>
        /// Speed of the Attack. Honestly have no idea where this would fit.
        /// </summary>
        protected float speed => _Speed;

        /// <summary>
        /// Active Effects of the Attack. **IS NOT CASTED AUTOMATICALLY, MUST BE ADDED ON YOUR OWN**
        /// </summary>
        protected ActiveEffect[] activeEffects => _ActiveEffects;

        public event Action<TrackAction> onActionTracked;

        /// <summary>
        /// Checks if the Attack can be used.
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool CanUse(CharacterBase attacker)
        {
            return !isActive && !_IsRestricted;
        }

        public void OnRestrict(RestrictAction restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictAction.Attack);
        }

        /// <summary>
        /// Starts Attack.
        /// </summary>
        /// <param name="attacker"></param>
        public virtual bool Use(CharacterBase attacker)
        {
            bool canUse = CanUse(attacker);

            if(canUse)
            {
                Begin(attacker);

                onActionTracked?.Invoke(TrackAction.Attack);
            }

            return canUse;
        }
    }
}