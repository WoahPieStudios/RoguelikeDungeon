using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Interfaces;

namespace Game.Characters
{
    public abstract class ActiveEffect : Effect, IActionRestricter
    {
        [SerializeField]
        RestrictAction _RestictAction;
        
        /// <summary>
        /// Restricts the actions of whomever is casted upon.
        /// </summary>
        public RestrictAction restrictAction => _RestictAction;

        /// <summary>
        /// Ends effect and is Removed from the effect list of whomever is casted upon.
        /// </summary>
        public override void End()
        {
            base.End();

            target.RemoveEffects(this);
        }
    }
}