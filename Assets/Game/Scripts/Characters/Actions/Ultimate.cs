using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate : CoolDownAction, IUltimateAction
    {
        [SerializeField]
        int _ManaCost;
        
        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimatex
        /// </summary>
        public int manaCost => _ManaCost;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onUseTrackableAction;

        protected override void Begin()
        {
            base.Begin();

            Mana mana = owner.GetProperty<Mana>();

            mana.UseMana(manaCost);

            onUseTrackableAction?.Invoke(TrackActionType.Ultimate);
        }

        /// <summary>
        /// Activates the Ultimate.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        public virtual bool Use()
        {
            Mana mana = owner.GetProperty<Mana>();

            bool canUse = !isActive && !isRestricted && !isCoolingDown && mana.currentMana >= manaCost;

            if(canUse)
                Begin();

            return canUse;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Ultimate);
        }
    }
}