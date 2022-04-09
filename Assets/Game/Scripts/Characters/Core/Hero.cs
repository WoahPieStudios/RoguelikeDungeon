using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Characters.Actions;
using Game.Characters.Effects;
using Game.Characters.Properties;

namespace Game.Characters
{
    public class Hero : Character, ITrackableActionsHandler
    {
        [SerializeField]
        Mana _Mana;
        [SerializeField]
        PassiveEffect[] _PassiveEffects;

        TrackActionType _TrackAction;
        List<ITrackableAction> _TrackableActionList = new List<ITrackableAction>();

        Dictionary<TrackActionType, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackActionType, PassiveEffect[]>();

        IHeroAttackAction _Attack;
        ISkillAction _Skill;
        IUltimateAction _Ultimate;

        public Mana mana => _Mana;

        public IHeroAttackAction attack => _Attack;
        
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
            _Skill = GetComponent<ISkillAction>();
            _Ultimate = GetComponent<IUltimateAction>();

            SegregatePassiveEffects(_PassiveEffects);

            AddTrackable(GetComponents<ITrackableAction>());
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
    }
}