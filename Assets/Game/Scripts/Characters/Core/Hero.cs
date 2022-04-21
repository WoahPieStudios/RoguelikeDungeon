using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Upgrades;
using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Properties;
using Game.Properties;

namespace Game.Characters
{
    public class Hero : Character, ITrackableActionsHandler, IUpgradeHandler
    {
        [SerializeField]
        Mana _Mana;
        [SerializeField]
        PassiveEffect[] _PassiveEffects;

        TrackActionType _TrackAction;

        List<ITrackableAction> _TrackableActionList = new List<ITrackableAction>();

        Dictionary<TrackActionType, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackActionType, PassiveEffect[]>();

        IHeroAttackAction _Attack;
        IHeroMovementAction _Movement; 
        ISkillAction _Skill;
        IUltimateAction _Ultimate;

        Dictionary<IProperty, float> _UpgradedProperties = new Dictionary<IProperty, float>();

        List<IUpgradeable> _Upgradeables = new List<IUpgradeable>();

        public Mana mana => _Mana;

        public IHeroAttackAction attack => _Attack;

        public IHeroMovementAction movement => _Movement;
        
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public ISkillAction skill => _Skill;

        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        public IUltimateAction ultimate => _Ultimate;

        public TrackActionType trackedActions => _TrackAction;

        protected override void Awake()
        {
            base.Awake();

            _Mana.ResetMana();

            _Attack = GetComponent<IHeroAttackAction>();
            _Movement = GetComponent<IHeroMovementAction>();
            _Skill = GetComponent<ISkillAction>();
            _Ultimate = GetComponent<IUltimateAction>();

            SegregatePassiveEffects(_PassiveEffects);

            AddTrackable(GetComponents<ITrackableAction>());

            foreach(IUpgradeable u in GetComponents<IUpgradeable>())
                _Upgradeables.Add(u);

            _Upgradeables.Add(health);
            _Upgradeables.Add(mana);
        }

        void AddTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => !_TrackableActionList.Contains(t)))
            {
                trackable.onUseTrackableAction += OnActionTracked;

                _TrackableActionList.Add(trackable);
            }
        }

        void RemoveTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => _TrackableActionList.Contains(t)))
            {
                trackable.onUseTrackableAction -= OnActionTracked;

                _TrackableActionList.Remove(trackable);
            }
        }
    
        void SegregatePassiveEffects(IEnumerable<PassiveEffect> passiveEffects)
        {
            foreach(Enum e in Enum.GetValues(typeof(TrackActionType)))
            {
                TrackActionType trackAction = (TrackActionType)e;

                PassiveEffect[] sortedPassiveEffects = passiveEffects.Where(p => p.trackAction.HasFlag(e)).ToArray();

                if(sortedPassiveEffects.Any())
                {
                    _TrackAction |= trackAction;

                    _TrackActionPassiveEffects.Add(trackAction, sortedPassiveEffects);
                }
            }
        }

        void OnActionTracked(TrackActionType trackAction)
        {
            foreach(Enum e in Enum.GetValues(typeof(TrackActionType)))
            {
                if(trackAction.HasFlag((TrackActionType)e) && _TrackActionPassiveEffects.ContainsKey(trackAction))
                {
                    PassiveEffect[] passiveEffects = _TrackActionPassiveEffects[trackAction];

                    if(passiveEffects.Any())
                        AddEffects(this, passiveEffects);

                    return;
                }
            }
        }

        public void Upgrade(IUpgradeable upgradeable, string property, float value)
        {
            if(!_Upgradeables.Contains(upgradeable))
            {
                Debug.LogAssertion($"[Upgrade Error] {upgradeable.GetType()} does not exist in {name}!");
                
                return;
            }
                
            if(!upgradeable.ContainsProperty(property))
            {
                Debug.LogAssertion($"[Upgrade Error] {property} does not exist in {upgradeable.GetType()}!");
                
                return;
            }

            IProperty p = upgradeable.GetProperty(property);

            if(!_UpgradedProperties.ContainsKey(p))
                _UpgradedProperties.Add(p, 0);

            float difference = value - _UpgradedProperties[p];
                
            _UpgradedProperties[p] += difference;

            p.valueAdded += difference;
        }

        public void Revert(IUpgradeable upgradeable, string property)
        {
            if(!_Upgradeables.Contains(upgradeable))
            {
                Debug.LogAssertion($"[Upgrade Error] {upgradeable.GetType()} does not exist in {name}!");
                
                return;
            }
                
            if(!upgradeable.ContainsProperty(property))
            {
                Debug.LogAssertion($"[Upgrade Error] {property} does not exist in {upgradeable.GetType()}!");
                
                return;
            }

            IProperty p = upgradeable.GetProperty(property);

            p.valueAdded -= _UpgradedProperties[p];

            _UpgradedProperties[p] = 0;
        }
    }
}