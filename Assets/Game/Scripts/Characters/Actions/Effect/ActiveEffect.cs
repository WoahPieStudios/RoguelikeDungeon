using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class ActiveEffect : Effect, IRestrainAction
    {
        // To Restrain an Action. I suggest you don't SerializeField a RestrictAction variable. My system expects all classes derived from this to have their own consistent RestrictAction. 
        // E.G. public override RestrictAction restrictAction => RestrictAction.Movement | RestrictAction.Skill;
        public abstract RestrictAction restrictAction { get; }

        public override void End()
        {
            base.End();

            characterBase.RemoveEffects(this);
        }
    }
}