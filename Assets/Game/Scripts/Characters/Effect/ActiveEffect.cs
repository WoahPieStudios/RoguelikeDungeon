using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;

namespace Game.Characters.Effects
{
    public abstract class ActiveEffect : Effect, IActionRestricter
    {
        [SerializeField]
        RestrictActionType _RestrictAction;
        
        /// <summary>
        /// Restricts the actions of whomever is casted upon.
        /// </summary>
        public RestrictActionType restrictAction => _RestrictAction;

        /// <summary>
        /// Ends effect and is Removed from the effect list of whomever is casted upon.
        /// </summary>
        public override void End()
        {
            sender.RemoveEffects(this);
        }
    }
}