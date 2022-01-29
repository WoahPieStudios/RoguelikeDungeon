using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class Ultimate : CoolDownAction, ITrackableAction, IRestrictableAction
    {
        [SerializeField]
        int _ManaCost;

        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimate
        /// </summary>
        public int manaCost => _ManaCost;

        public event Action<TrackAction> onActionTracked;

        /// <summary>
        /// Activates the Ultimate.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        public virtual bool Use(Hero hero)
        {
            bool canUse = CanUse(hero);

            if(canUse)
            {
                hero.mana.UseMana(_ManaCost);
                
                Begin(hero);

                onActionTracked?.Invoke(TrackAction.Ultimate);
            }

            return canUse;
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the ultimate can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        /// <returns></returns>
        public virtual bool CanUse(Hero hero)
        {
            return !_IsRestricted && !isActive && hero.mana.currentMana >= _ManaCost && !isCoolingDown;
        }

        public void OnRestrict(RestrictAction restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictAction.Ultimate);
        }
    }
}