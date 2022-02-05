using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Effects
{
    public abstract class PassiveEffect : Effect, IPassiveEffect
    {
        [SerializeField]
        TrackActionType _TrackAction;
        
        /// <summary>
        /// Flags the Actions of the Hero to be tracked.
        /// </summary>
        public TrackActionType trackAction => _TrackAction;
        
        /// <summary>
        /// Ends Passive Effect.
        /// </summary>
        public override void End()
        {
            receiver.RemoveEffects(this);
        }
    }
}