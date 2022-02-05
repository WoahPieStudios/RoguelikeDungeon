using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Skill : CoolDownAction, ISkillAction
    {
        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onUseTrackableAction;
        public event Action<IPriorityAction> onUsePriorityAction;

        protected virtual void OnActionEvent(TrackActionType trackAction)
        {
            onUseTrackableAction?.Invoke(trackAction);
        }

        protected override void Begin()
        {
            base.Begin();
            
            onUseTrackableAction?.Invoke(TrackActionType.Skill);

            onUsePriorityAction?.Invoke(this);
        }

        /// <summary>
        /// Start Skill.
        /// </summary>
        /// <param name="hero">The Hero who will use the Skill</param>
        public virtual bool Use()
        {
            bool canUse = !isActive && !isCoolingDown && !isRestricted;

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