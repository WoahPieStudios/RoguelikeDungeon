using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Skill : CoolDownAction, ISkillAction, ITrackableAction, IRestrictableAction
    {
        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onActionEvent;

        protected virtual void OnActionEvent(TrackActionType trackAction)
        {
            onActionEvent?.Invoke(trackAction);
        }

        protected override void Begin()
        {
            base.Begin();

            OnActionEvent(TrackActionType.Skill);
        }

        /// <summary>
        /// Start Skill.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        public virtual bool Use()
        {
            bool canUse = CanUse();

            if(canUse)
                Begin();

            return canUse;
        }

        // To check if it can be used. VERY IMPORTANT. Actually everything is important. 
        /// <summary>
        /// Checks if the Skill can be used.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        /// <returns></returns>
        public virtual bool CanUse()
        {
            return !isActive && !isCoolingDown && !isRestricted;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}