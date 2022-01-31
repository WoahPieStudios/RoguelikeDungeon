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
        Health _Health;
        [SerializeField]
        Mana _Mana;
        [SerializeField]
        PassiveEffect[] _PassiveEffects;

        TrackActionType _TrackAction;
        List<ITrackableAction> _TrackableActionList = new List<ITrackableAction>();

        Dictionary<TrackActionType, PassiveEffect[]> _TrackActionPassiveEffects = new Dictionary<TrackActionType, PassiveEffect[]>();

        Attack _Attack;
        Skill _Skill;
        Ultimate _Ultimate;

        public Health health => _Health;

        public Mana mana => _Mana;

        public Attack attack => _Attack;
        
        /// <summary>
        /// Skill of the Hero
        /// </summary>
        public Skill skill => _Skill;

        /// <summary>
        /// Ultimate of the Hero
        /// </summary>
        public Ultimate ultimate => _Ultimate;

        public ITrackableAction[] trackableActions => _TrackableActionList.ToArray();

        public TrackActionType trackedActions => _TrackAction;

        protected override void Awake()
        {
            base.Awake();

            _Health.owner = this;
            _Health.ResetHealth();

            _Mana.owner = this;
            _Mana.ResetMana();

            _Attack = GetProperty<Attack>();
            _Skill = GetProperty<Skill>();
            _Ultimate = GetProperty<Ultimate>();

            SegregatePassiveEffects(_PassiveEffects);

            AddProperty(_Health, _Mana);

            AddTrackable(GetProperties<ITrackableAction>());
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
                        AddEffects(this, passiveEffects.Where(p => p.CanUse(this)).ToArray());

                    return;
                }
            }
        }

        public void AddTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => !_TrackableActionList.Contains(t)))
            {
                trackable.onActionEvent += OnActionTracked;

                _TrackableActionList.Add(trackable);
            }
        }

        public void RemoveTrackable(params ITrackableAction[] trackables)
        {
            foreach(ITrackableAction trackable in trackables.Where(t => _TrackableActionList.Contains(t)))
            {
                trackable.onActionEvent -= OnActionTracked;

                _TrackableActionList.Remove(trackable);
            }
        }
    }
}