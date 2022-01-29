using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class Skill : CoolDownAction, ITrackableAction, IRestrictableAction
    {
        bool _IsRestricted = false;

        public event Action<TrackAction> onActionTracked;

        /// <summary>
        /// Start Skill.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        public virtual bool Use(Hero hero)
        {
            bool canUse = CanUse(hero);

            if(canUse)
            {
                Begin(hero);

                onActionTracked?.Invoke(TrackAction.Skill);
            }

            return canUse;
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the Skill can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        /// <returns></returns>
        public virtual bool CanUse(Hero hero)
        {
            return !isActive && !isCoolingDown && !_IsRestricted;
        }

        public void OnRestrict(RestrictAction restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictAction.Skill);
        }
    }
}