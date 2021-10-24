using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class PassiveEffect : Effect, ITrackAction
    {
        // To Track an Action. I suggest you don't SerializeField a TrackAction variable. My system expects all classes derived from this to have their own consistent TrackAction. 
        // E.G. public override TrackAction trackAction => TrackAction.Skill | TrackAction.Ultiamate;
        public abstract TrackAction trackAction { get; }

        public virtual bool CanUse(Hero hero)
        {
            return !isActive;
        }
        
        public override void End()
        {
            base.End();

            characterBase.RemoveEffects(this);
        }
    }
}