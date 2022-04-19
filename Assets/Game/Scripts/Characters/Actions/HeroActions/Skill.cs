using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game.Characters.Actions
{
    public abstract class Skill<T> : HeroCoolDownAction<T>, ISkillAction where T : Hero
    {
        bool _IsRestricted = false;

        public bool isRestricted => _IsRestricted;

        public event System.Action<TrackActionType> onUseTrackableAction;

        protected override void OnUse()
        {
            base.OnUse();
            
            onUseTrackableAction?.Invoke(TrackActionType.Skill);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted;
        }
        
        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Skill);
        }
    }
}