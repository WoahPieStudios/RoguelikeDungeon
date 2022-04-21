using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;
using Game.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate<T> : CoolDownAction<T>, IUltimateAction where T : Hero
    {
        [SerializeField]
        Property _ManaCost = new Property(ManaCostProperty);
        
        bool _IsRestricted = false;

        /// <summary>
        /// Mana cost of the Ultimatex
        /// </summary>
        public Property manaCost => _ManaCost;

        public bool isRestricted => _IsRestricted;

        public event Action<TrackActionType> onUseTrackableAction;

        public const string ManaCostProperty = "manaCost";

        protected override void Awake()
        {
            base.Awake();

            propertyList.Add(manaCost);
        }

        protected override void OnUse()
        {
            base.OnUse();

            Mana mana = owner.mana;

            mana.UseMana(manaCost);

            onUseTrackableAction?.Invoke(TrackActionType.Ultimate);
        }

        protected override bool CanUse()
        {
            return base.CanUse() && !isRestricted && !isCoolingDown && owner.mana.currentMana >= manaCost;
        }
        
        public void OnRestrict(RestrictActionType restrictActions)
        {
            _IsRestricted = restrictActions.HasFlag(RestrictActionType.Ultimate);
        }
    }
}