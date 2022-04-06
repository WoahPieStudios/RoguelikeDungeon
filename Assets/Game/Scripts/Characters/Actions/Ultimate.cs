using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate : CoolDownAction<Hero>
    {
        [SerializeField]
        float _ManaCost;
        
        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimatex
        /// </summary>
        public float manaCost => _ManaCost;

        public bool isRestricted => _IsRestricted;

        public event System.Action<TrackActionType> onUseTrackableAction;

        protected override void Begin()
        {
            base.Begin();

            Mana mana = owner.mana;

            mana.UseMana(manaCost);

            onUseTrackableAction?.Invoke(TrackActionType.Ultimate);
        }

        /// <summary>
        /// Activates the Ultimate.
        /// </summary>
        /// <param name="hero">The Hero who will use the Ultimate</param>
        public virtual bool Use()
        {
            bool canUse = !isActive && !isRestricted && !isCoolingDown && owner.mana.currentMana >= manaCost;

            if(canUse)
                Begin();

            return canUse;
        }

        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Ultimate);
        }

        public override void Upgrade(string property, object value)
        {
            base.Upgrade(property, value);

            switch(property)
            {
                case "manaCost":
                    if(value is float v)
                        _ManaCost = v;
                    break;
            }
        }
    }
}