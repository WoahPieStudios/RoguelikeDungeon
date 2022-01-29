using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class PassiveEffect : Effect, ITrackActionEffect
    {
        [SerializeField]
        TrackAction _TrackAction;
        
        /// <summary>
        /// Flags the Actions of the Hero to be tracked.
        /// </summary>
        public TrackAction trackAction => _TrackAction;

        /// <summary>
        /// Checks if the passive effect can be used.
        /// </summary>
        public virtual bool CanUse(Hero hero)
        {
            return !isActive;
        }
        
        /// <summary>
        /// Ends Passive Effect.
        /// </summary>
        public override void End()
        {
            base.End();

            target.RemoveEffects(this);
        }
    }
}