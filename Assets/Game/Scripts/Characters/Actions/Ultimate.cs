using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Properties;

namespace Game.Characters.Actions
{
    public abstract class Ultimate<T> : HeroCoolDownAction<T>, IUltimateAction where T : Hero
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

        public const string ManaCostProperty = "manaCost";

        protected override void Awake()
        {
            base.Awake();
            
            SetPropertyStartValue(ManaCostProperty, _ManaCost);
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

        public override bool Contains(string property)
        {
            return base.Contains(property) || property switch
            {
                ManaCostProperty => true,
                _ => false 
            };
        }
    }
}