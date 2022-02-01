using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate : CoolDownAction, IUltimateAction, ITrackableAction, IRestrictableAction
    {
        [SerializeField]
        int _ManaCost;

        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimate
        /// </summary>
        public int manaCost => _ManaCost;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onActionEvent;

        protected virtual void OnActionEvent(TrackActionType trackAction)
        {
            onActionEvent?.Invoke(trackAction);
        }

        protected override void Begin()
        {
            base.Begin();

            Mana mana = owner.GetProperty<Mana>();

            mana.UseMana(_ManaCost);

            OnActionEvent(TrackActionType.Ultimate);
        }

        /// <summary>
        /// Activates the Ultimate.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        public virtual bool Use()
        {
            bool canUse = CanUse();

            if(canUse)
                Begin();

            return canUse;
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the ultimate can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        /// <returns></returns>
        public virtual bool CanUse()
        {
            Mana mana = owner.GetProperty<Mana>();

            return !isRestricted && !isActive && mana.currentMana >= _ManaCost && !isCoolingDown;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Ultimate);
        }
    }
}