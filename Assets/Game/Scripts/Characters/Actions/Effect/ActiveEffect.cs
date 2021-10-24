using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public abstract class ActiveEffect : Effect
    {
        [SerializeField]
        RestrictAction _RestrictAction;

        public RestrictAction restrictAction => _RestrictAction;

        public override void End()
        {
            base.End();

            characterBase.RemoveEffects(this);
        }
    }
}