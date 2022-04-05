using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Skill : CoolDownAction<Hero>
    {
        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public event System.Action<TrackActionType> onUseTrackableAction;

        protected override void Begin()
        {
            base.Begin();
            
            onUseTrackableAction?.Invoke(TrackActionType.Skill);
        }

        /// <summary>
        /// Start Skill.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        public virtual bool Use()
        {
            bool canUse = !isActive && !isRestricted && !isCoolingDown;

            if(canUse)
                Begin();

            return canUse;
        }
        
        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}